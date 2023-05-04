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

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog1.FileName);

                pictureBox1.Image = originalImage;
            }
        }
        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ОпцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem.Checked = false;
            сделатьСплошнымToolStripMenuItem.Checked = false;
            осветлитьToolStripMenuItem.Checked = false;

            (sender as ToolStripMenuItem).Checked = true;
        }

        private void ОбработатьФонButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || backgroundColor == Color.Empty)
                return;

            ImageProcessing imageProcessing = new ImageProcessing(pictureBox1.Image, trackBar2.Value);

            if (удалитьToolStripMenuItem.Checked)
            {
                var sw = Stopwatch.StartNew();
                pictureBox1.Image = imageProcessing.DeleteBackground(backgroundColor);
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
                    pictureBox1.Image = imageProcessing.MakeSolidBackground(backgroundColor, color);
                    sw.Stop();
                    MessageBox.Show("Обработка изображения выполнена за " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.", "Обработка завершена",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (осветлитьToolStripMenuItem.Checked)
            {
                var sw = Stopwatch.StartNew();
                pictureBox1.Image = imageProcessing.LightenBackground(backgroundColor);
                sw.Stop();
                MessageBox.Show("Обработка изображения выполнена за " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.", "Обработка завершена",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void СброситьИзмененияButton_Click(object sender, EventArgs e)
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
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
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
