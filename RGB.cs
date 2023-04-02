using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace removebackground
{
    public partial class RGB : Form
    {
        public RGB()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(RedTrackBar.Value, GreenTrackBar.Value, BlueTrackBar.Value);
            RedTextBox.Text = RedTrackBar.Value.ToString();
            GreenTextBox.Text = GreenTrackBar.Value.ToString();
            BlueTextBox.Text = BlueTrackBar.Value.ToString();
        }
    }
}
