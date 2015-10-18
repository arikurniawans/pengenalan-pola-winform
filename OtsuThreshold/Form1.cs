﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OtsuThreshold
{
    public partial class Form1 : Form
    {
        private Otsu ot = new Otsu();
        private Thining th = new Thining();
        private Bitmap org,org2;

        private Histograma.HistogramaDesenat Histogram;

        public Form1()
        {
            InitializeComponent();
            // Histogram
            // 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                org = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }*/

            using (SaveFileDialog sfdlg = new SaveFileDialog())
            {
                sfdlg.Title = "Save Dialog";
                sfdlg.Filter = "Bitmap Images (*.bmp)|*.bmp|All files(*.*)|*.*";
                if (sfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        //pictureBox1.Image.Save(sfdlg.FileName);
                        //bmp.Save(sfdlg.FileName);
                        
                        ot.Save(bmp, pictureBox1.Width, pictureBox1.Height, 50, sfdlg.FileName);
                        MessageBox.Show("Saved Successfully.....");

                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap temp = (Bitmap)org.Clone();
            ot.Convert2GrayScaleFast(temp);
            int otsuThreshold= ot.getOtsuThreshold((Bitmap)temp);
            ot.threshold(temp,otsuThreshold);
            textBox1.Text = otsuThreshold.ToString();
            
            pictureBox2.Image = temp;
            //pictureBox1.Image.Dispose();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            
            Bitmap temp2 = (Bitmap)org.Clone();
            ot.invers((Bitmap)temp2, 2);

            pictureBox2.Image = temp2;
            //pictureBox1.Image.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            org = (Bitmap)pictureBox1.Image.Clone();
            //org2 = (Bitmap)pictureBox2.Image.Clone();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Bitmap temp = (Bitmap)org.Clone();
            ot.Convert2GrayScaleFast(temp);
            int otsuThreshold = ot.getOtsuThreshold((Bitmap)temp);
            ot.threshold(temp, otsuThreshold);

            //Bitmap temp2 = (Bitmap)org.Clone();
            ot.invers(temp, 2);


            bool[][] t = th.ImageCheckToBool(temp);
            t = th.ZhangSuenThinning(t);
            pictureBox2.Image = th.Bool2Image(t);
        }

        
        }
    
}