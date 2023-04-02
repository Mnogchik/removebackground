using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace removebackground
{
    public partial class Form1 : Form
    {
        Point rectStartPoint;
        Rectangle rect = new Rectangle();
        private readonly Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        Bitmap originalImage = new Bitmap(Properties.Resources._1587229750_7_p_foni_s_zhivotnimi_dlya_windows_11);

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            rectStartPoint = e.Location;
            Invalidate();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point rectEndPoint = e.Location;
            rect.Location = new Point(
                Math.Min(rectStartPoint.X, rectEndPoint.X),
                Math.Min(rectStartPoint.Y, rectEndPoint.Y));
            rect.Size = new Size(
                Math.Abs(rectStartPoint.X - rectEndPoint.X),
                Math.Abs(rectStartPoint.Y - rectEndPoint.Y));
            pictureBox1.Invalidate();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (rect != null && rect.Width > 0 && rect.Height > 0)
                {
                    e.Graphics.FillRectangle(selectionBrush, rect);
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog1.FileName);

                pictureBox1.Image = originalImage;
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(saveFileDialog1.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem.Checked = true;
            сделатьСплошнымToolStripMenuItem.Checked = false;
            осветлитьToolStripMenuItem.Checked = false;
        }
        private void сделатьСплошнымToolStripMenuItem_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem.Checked = false;
            сделатьСплошнымToolStripMenuItem.Checked = true;
            осветлитьToolStripMenuItem.Checked = false;
        }
        private void осветлитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem.Checked = false;
            сделатьСплошнымToolStripMenuItem.Checked = false;
            осветлитьToolStripMenuItem.Checked = true;
        }
        private async void обработатьФонButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            if (rect == Rectangle.Empty || rect.Top < 0 || rect.Left < 0
                || 1f * rect.Right * pictureBox1.Image.Width / pictureBox1.ClientSize.Width > pictureBox1.Image.Width
                || 1f * rect.Bottom * pictureBox1.Image.Height / pictureBox1.ClientSize.Height > pictureBox1.Image.Height)
            {
                MessageBox.Show("Область обработки некорректна", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap image = new Bitmap(pictureBox1.Image);

            // доступ к UI программы во время обработки изображения
            await Task.Run(() => {
                if (удалитьToolStripMenuItem.Checked)
                {
                    var sw = Stopwatch.StartNew();
                    pictureBox1.Image = DeleteBackground(image);
                    sw.Stop();
                    MessageBox.Show("Обработка изображения выполнена за " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.", "Обработка завершена",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (сделатьСплошнымToolStripMenuItem.Checked)
                {
                    RGB rgbForm = new RGB();

                    if (rgbForm.ShowDialog() == DialogResult.OK)
                    {
                        var color = Color.FromArgb(rgbForm.RedTrackBar.Value, rgbForm.GreenTrackBar.Value, rgbForm.BlueTrackBar.Value);

                        var sw = Stopwatch.StartNew();
                        pictureBox1.Image = MakeSolidBackground(image, color);
                        sw.Stop();
                        MessageBox.Show("Обработка изображения выполнена за " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.", "Обработка завершена",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else if (осветлитьToolStripMenuItem.Checked)
                {
                    var sw = Stopwatch.StartNew();
                    pictureBox1.Image = LightenBackground(image);
                    sw.Stop();
                    MessageBox.Show("Обработка изображения выполнена за " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.", "Обработка завершена",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }
        private void сброситьИзмененияButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите сбросить изменения?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    pictureBox1.Image = originalImage;
                }
            }
        }

        private Bitmap DeleteBackground(Bitmap image)
        {
            List<Pixel> pixels = GetPixels(image); // список всех пикселей картинки
            List<Pixel> rectPixels = GetRectPixels(rect, image); // список пикселей прямоугольника
            List<Pixel> background = GetBackground(pixels, rectPixels); // список пикселей заднего фона

            for (int i = 0; i < background.Count; i++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, Color.Transparent);
            }

            return image;
        }
        private Bitmap MakeSolidBackground(Bitmap image, Color color)
        {
            List<Pixel> pixels = GetPixels(image);
            List<Pixel> rectPixels = GetRectPixels(rect, image);
            List<Pixel> background = GetBackground(pixels, rectPixels);

            for (int i = 0; i < background.Count; i ++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, color);
            }

            return image;
        }
        private Bitmap LightenBackground(Bitmap image)
        {
            List<Pixel> pixels = GetPixels(image);
            List<Pixel> rectPixels = GetRectPixels(rect, image);
            List<Pixel> background = GetBackground(pixels, rectPixels);

            for (int i = 0; i < background.Count; i++)
            {
                var R = background[i].Color.R + (0.25 * (255 - background[i].Color.R));
                var G = background[i].Color.G + (0.25 * (255 - background[i].Color.G));
                var B = background[i].Color.B + (0.25 * (255 - background[i].Color.B));
                image.SetPixel(background[i].Point.X, background[i].Point.Y, Color.FromArgb((int)R, (int)G, (int)B));
            }

            return image;
        }

        // создает список структур ВСЕХ пикселей изображения с информацией о их координатах и цвете
        private List<Pixel> GetPixels(Bitmap bitmap)
        {
            var pixels = new List<Pixel>(bitmap.Width * bitmap.Height);

            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                    pixels.Add(new Pixel()
                    {
                        Color = bitmap.GetPixel(x, y),
                        Point = new Point() { X = x, Y = y }
                    });
            return pixels;
        }
        private List<Pixel> GetRectPixels(Rectangle rect, Bitmap bitmap)
        {
            List<Pixel> rectPixels = new List<Pixel>(bitmap.Width * bitmap.Height);

            // соотношение реальной ширины картинки к масштабу в PictureBox
            float stretch1X = 1f * pictureBox1.Image.Width / pictureBox1.ClientSize.Width;
            float stretch1Y = 1f * pictureBox1.Image.Height / pictureBox1.ClientSize.Height;

            // нахождение координат прямоугольника относительно PictureBox
            int rect_left = (int)(rect.Left * stretch1X);
            int rect_top = (int)(rect.Top * stretch1Y);
            int rect_right = (int)(rect.Right * stretch1X);
            int rect_bottom = (int)(rect.Bottom * stretch1Y);

            for (int x = rect_left; x < rect_right; x++)
                for (int y = rect_top; y < rect_bottom; y++)
                {
                    rectPixels.Add(new Pixel()
                    {
                        Color = bitmap.GetPixel(x, y),
                        Point = new Point() { X = x, Y = y }
                    });
                }
            
            return rectPixels;
        }

        //возвращает список пикселей, которые являются частью фона(основной алгоритм)
        private List<Pixel> GetBackground(List<Pixel> allPixels, Color backgroundColor)
        {
            List<Pixel> backgroundPixels = new List<Pixel>(allPixels.Count);

            int threshold = trackBar2.Value * 10;

            foreach (var pixel in allPixels)
            {
                Color pixelColor = pixel.Color;

                if (ColorDistance(pixelColor, backgroundColor) < threshold)
                {
                    backgroundPixels.Add(pixel);
                }

            }

            return backgroundPixels;
        }
        private static int ColorDistance(Color c1, Color c2)
        {
            int rDiff = c1.R - c2.R;
            int gDiff = c1.G - c2.G;
            int bDiff = c1.B - c2.B;

            return (int)Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }
    }
}
