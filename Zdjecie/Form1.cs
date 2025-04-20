namespace Zdjecie
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;

        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap loaded = new Bitmap(openFileDialog.FileName);
                originalImage = ResizeImage(loaded, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = originalImage;

            }
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            if (originalImage == null)
                return;

            // kopie dla ka¿dego w¹tku
            Bitmap img1 = new Bitmap(originalImage);
            Bitmap img2 = new Bitmap(originalImage);
            Bitmap img3 = new Bitmap(originalImage);
            Bitmap img4 = new Bitmap(originalImage);

            // lambda przekazuje watkowi co ma robic
            Thread t1 = new Thread(() =>
            {
                var img = ApplyGrayscale(img1);
                // invoke -> operacaj przez glowny watek bo windows form to model jednowatkowy 
                pictureBox2.Invoke(() => pictureBox2.Image = img);
            });

            Thread t2 = new Thread(() =>
            {
                var img = ApplyNegative(img2);
                pictureBox3.Invoke(() => pictureBox3.Image = img);
            });

            Thread t3 = new Thread(() =>
            {
                var img = ApplyThreshold(img3, 100);
                pictureBox4.Invoke(() => pictureBox4.Image = img);
            });

            Thread t4 = new Thread(() =>
            {
                var img = ApplyMirror(img4);
                pictureBox5.Invoke(() => pictureBox5.Image = img);
            });

            t1.Start(); t2.Start(); t3.Start(); t4.Start();
        }


        // Filtry
        private Bitmap ApplyGrayscale(Bitmap img)
        {
            Bitmap result = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    result.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return result;
        }

        private Bitmap ApplyNegative(Bitmap img)
        {
            Bitmap result = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            return result;
        }

        private Bitmap ApplyThreshold(Bitmap img, int threshold)
        {
            Bitmap result = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    int avg = (c.R + c.G + c.B) / 3;
                    int val = 0;
                    if (avg > threshold)
                        val = 255;
                    result.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
            return result;
        }

        private Bitmap ApplyMirror(Bitmap img)
        {
            Bitmap result = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    result.SetPixel(img.Width - x - 1, y, img.GetPixel(x, y));
                }
            return result;
        }

        private Bitmap ResizeImage(Bitmap img, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return result;
        }


        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
 
            button1.Location = new Point(100, 382);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Wczytaj";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
  
            button2.Location = new Point(295, 240);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 1;
            button2.Text = "Uruchom";
            button2.UseVisualStyleBackColor = true;
 
            pictureBox1.Location = new Point(24, 143);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(254, 199);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
 
            pictureBox2.Location = new Point(418, 30);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(254, 199);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
    
            pictureBox3.Location = new Point(693, 30);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(254, 199);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
 
            pictureBox4.Location = new Point(693, 279);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(254, 199);
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
 
            pictureBox5.Location = new Point(418, 279);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(254, 199);
            pictureBox5.TabIndex = 6;
            pictureBox5.TabStop = false;
 
            label1.AutoSize = true;
            label1.Location = new Point(129, 101);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 7;
            label1.Text = "Oryginalny";
 
            label2.AutoSize = true;
            label2.Location = new Point(516, 7);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 8;
            label2.Text = "GrayScale";
            label2.Click += label2_Click;
 
            label3.AutoSize = true;
            label3.Location = new Point(791, 7);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 9;
            label3.Text = "Negatyw";
 
            label4.AutoSize = true;
            label4.Location = new Point(481, 249);
            label4.Name = "label4";
            label4.Size = new Size(127, 20);
            label4.TabIndex = 10;
            label4.Text = "Odbicie Lustrzane";
            label4.Click += label4_Click;
   
            label5.AutoSize = true;
            label5.Location = new Point(791, 256);
            label5.Name = "label5";
            label5.Size = new Size(88, 20);
            label5.TabIndex = 11;
            label5.Text = "Progowanie";
            label5.Click += label5_Click;
 
            ClientSize = new Size(975, 545);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
