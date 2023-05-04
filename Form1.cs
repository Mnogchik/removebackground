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

namespace removebackground
{
    public partial class Form1 : Form
    {
        Color backgroundColor = Color.Empty;
        Bitmap originalImage;

        public Form1()
        {
            InitializeComponent();

            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            originalImage = new Bitmap((Bitmap)resources.GetObject("pictureBox1.Image"));
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
                        MessageBox.Show("Изображение не удалось сохранить", "Ошибка",
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
        private void обработатьФонButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || backgroundColor == Color.Empty)
                return;

            Bitmap image = new Bitmap(pictureBox1.Image);


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
            List<Pixel> background = GetBackground(pixels, backgroundColor); // список пикселей заднего фона

            for (int i = 0; i < background.Count; i++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, Color.Transparent);
            }

            return image;
        }
        private Bitmap MakeSolidBackground(Bitmap image, Color color)
        {
            List<Pixel> pixels = GetPixels(image);
            List<Pixel> background = GetBackground(pixels, backgroundColor);

            for (int i = 0; i < background.Count; i ++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, color);
            }

            return image;
        }
        private Bitmap LightenBackground(Bitmap image)
        {
            List<Pixel> pixels = GetPixels(image);
            List<Pixel> background = GetBackground(pixels, backgroundColor);

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

        //возвращает список пикселей, которые являются частью фона(основной алгоритм)
        private List<Pixel> GetBackground(List<Pixel> allPixels, Color backgroundColor)
        {
            List<Pixel> backgroundPixels = new List<Pixel>(allPixels.Count);

            int threshold = trackBar2.Value;

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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point coord = GetRealCoords(e.X, e.Y);
            backgroundColor = new Bitmap(pictureBox1.Image).GetPixel(coord.X, coord.Y);

            panel1.BackColor = backgroundColor;
        }
        private Point GetRealCoords(int x, int y)
        {
            return new Point() { X = (int)(1f * x * pictureBox1.Image.Width / pictureBox1.ClientSize.Width), Y = (int)(1f * y * pictureBox1.Image.Height / pictureBox1.ClientSize.Height) };
        }
    }
}
