using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDI绘制验证码
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string s = null;
            for (int i = 0; i <5;i ++)
            {
                s += r.Next(0, 10).ToString();
            }
          
            //创建GDI对象
            Bitmap bmp = new Bitmap(150, 40);
            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < 5; i++)
            {
                Point p = new Point(i * 20, 0);
                String[] fonts = { "宋体", "隶书", "仿宋", "黑体", "微软雅黑" };
                Color[] colors = { Color.Black, Color.Blue, Color.Red, Color.Yellow, Color.Green };
                g.DrawString(s[i].ToString(), new Font(fonts[r.Next(0, 5)], 20, FontStyle.Bold), new SolidBrush(colors[r.Next(0, 5)]),p);
            }
            //将图片镶嵌到 pictureBox中
            pictureBox1.Image = bmp;
            for (int i = 0; i < 20; i++)
            {
                Point p1 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                Point p2 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                Pen pen = new Pen(Brushes.Gray);
                g.DrawLine(pen,p1, p2);
            }
            for (int i = 0; i < 500; i++)
            {
                Point p = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                bmp.SetPixel(p.X,p.Y,Color.Green);
            }
        }
    }
}
