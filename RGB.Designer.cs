
namespace removebackground
{
    partial class RGB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RedTrackBar = new System.Windows.Forms.TrackBar();
            this.RedTextBox = new System.Windows.Forms.TextBox();
            this.BlueTextBox = new System.Windows.Forms.TextBox();
            this.GreenTextBox = new System.Windows.Forms.TextBox();
            this.применитьButton = new System.Windows.Forms.Button();
            this.GreenTrackBar = new System.Windows.Forms.TrackBar();
            this.BlueTrackBar = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(101, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(32, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "RED";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(32, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "GREEN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(32, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "BLUE";
            // 
            // RedTrackBar
            // 
            this.RedTrackBar.Location = new System.Drawing.Point(101, 142);
            this.RedTrackBar.Maximum = 255;
            this.RedTrackBar.Name = "RedTrackBar";
            this.RedTrackBar.Size = new System.Drawing.Size(258, 45);
            this.RedTrackBar.TabIndex = 4;
            this.RedTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // RedTextBox
            // 
            this.RedTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RedTextBox.Location = new System.Drawing.Point(365, 142);
            this.RedTextBox.Multiline = true;
            this.RedTextBox.Name = "RedTextBox";
            this.RedTextBox.ReadOnly = true;
            this.RedTextBox.Size = new System.Drawing.Size(54, 33);
            this.RedTextBox.TabIndex = 7;
            this.RedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BlueTextBox
            // 
            this.BlueTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BlueTextBox.Location = new System.Drawing.Point(365, 241);
            this.BlueTextBox.Multiline = true;
            this.BlueTextBox.Name = "BlueTextBox";
            this.BlueTextBox.ReadOnly = true;
            this.BlueTextBox.Size = new System.Drawing.Size(54, 33);
            this.BlueTextBox.TabIndex = 8;
            this.BlueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GreenTextBox
            // 
            this.GreenTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GreenTextBox.Location = new System.Drawing.Point(365, 190);
            this.GreenTextBox.Multiline = true;
            this.GreenTextBox.Name = "GreenTextBox";
            this.GreenTextBox.ReadOnly = true;
            this.GreenTextBox.Size = new System.Drawing.Size(54, 33);
            this.GreenTextBox.TabIndex = 9;
            this.GreenTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // применитьButton
            // 
            this.применитьButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.применитьButton.Location = new System.Drawing.Point(159, 299);
            this.применитьButton.Name = "применитьButton";
            this.применитьButton.Size = new System.Drawing.Size(138, 39);
            this.применитьButton.TabIndex = 10;
            this.применитьButton.Text = "Применить";
            this.применитьButton.UseVisualStyleBackColor = true;
            // 
            // GreenTrackBar
            // 
            this.GreenTrackBar.Location = new System.Drawing.Point(101, 190);
            this.GreenTrackBar.Maximum = 255;
            this.GreenTrackBar.Name = "GreenTrackBar";
            this.GreenTrackBar.Size = new System.Drawing.Size(258, 45);
            this.GreenTrackBar.TabIndex = 11;
            this.GreenTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // BlueTrackBar
            // 
            this.BlueTrackBar.Location = new System.Drawing.Point(101, 241);
            this.BlueTrackBar.Maximum = 255;
            this.BlueTrackBar.Name = "BlueTrackBar";
            this.BlueTrackBar.Size = new System.Drawing.Size(258, 45);
            this.BlueTrackBar.TabIndex = 12;
            this.BlueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RGB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 350);
            this.Controls.Add(this.BlueTrackBar);
            this.Controls.Add(this.GreenTrackBar);
            this.Controls.Add(this.применитьButton);
            this.Controls.Add(this.GreenTextBox);
            this.Controls.Add(this.BlueTextBox);
            this.Controls.Add(this.RedTextBox);
            this.Controls.Add(this.RedTrackBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "RGB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RGB";
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TrackBar RedTrackBar;
        private System.Windows.Forms.Button применитьButton;
        public System.Windows.Forms.TrackBar GreenTrackBar;
        public System.Windows.Forms.TrackBar BlueTrackBar;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TextBox RedTextBox;
        public System.Windows.Forms.TextBox BlueTextBox;
        public System.Windows.Forms.TextBox GreenTextBox;
    }
}