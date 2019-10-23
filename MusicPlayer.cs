using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace 音乐播放器1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusicPlayer.Ctlcontrols.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            MusicPlayer.settings.autoStart = false;
            MusicPlayer.URL = @"C:\Users\hp\Desktop\音乐\薛之谦 - 暧昧.flac";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "播放")
            {
                MusicPlayer.Ctlcontrols.play();
                button1.Text = "暂停";
            }
            else if (button1.Text == "暂停")
            {
                MusicPlayer.Ctlcontrols.pause();
                button1.Text = "播放";
            }
        }
        //存储音乐文件的全路径
        List<string> listpath = new List<string>();
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = @"C:\Users\hp\Desktop\音乐";
            of.Title = "请选择音乐文件哦O(∩_∩)O";
            //of.Filter = "音乐文件|*.wav|MP3文件|*.mp3|所有文件|*.*";
            of.Multiselect = true;
            of.ShowDialog();

            //获得所选文件在文本框中的全路径
            string[] path = of.FileNames;
            for (int i = 0; i < path.Length; i++)
            {
                //将音乐文件的全路径存储到泛型容器中
                listpath.Add(path[i]);

                //将音乐文件的文件名存储到ListBox中
                listBox1.Items.Add(Path.GetFileName(path[i]));
            }
        }
        //为ListBox1创建双击事件  双击播放选中的音乐
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("请先选择音乐文件");
                return;
            }
            try  //防止用户点到空白部分，使ListBox1的索引值为-1，程序抛异常
            {
                MusicPlayer.URL = listpath[listBox1.SelectedIndex];
                MusicPlayer.Ctlcontrols.play();
                button1.Text = "暂停";
            }
            catch { }
        }
        //下一曲
        private void button4_Click(object sender, EventArgs e)
        {
            //获得当前选中项的索引
            int index = listBox1.SelectedIndex;
            //清空已经选中的索引
            listBox1.SelectedIndices.Clear();
            index++;
            //播放到最后一曲时回到第一曲
            if (index == listBox1.Items.Count)
            {
                index = 0;
            }
            //将当前索引赋值给选中的索引
            listBox1.SelectedIndex = index;
            MusicPlayer.URL = listpath[index];
            MusicPlayer.Ctlcontrols.play();
        }
        //上一曲
        private void button3_Click(object sender, EventArgs e)
        {
            //获得当前选中项的索引
            int index = listBox1.SelectedIndex;
            //清空已经选中的索引
            listBox1.SelectedIndices.Clear();
            index--;
            //播放到第一曲时回到最后一曲
            if (index < 0)
            {
                index = listBox1.Items.Count - 1;
            }
            //将当前索引赋值给选中的索引
            listBox1.SelectedIndex = index;
            MusicPlayer.URL = listpath[index];
            MusicPlayer.Ctlcontrols.play();
        }
        //设置右击 多选删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获得选中项的个数
            int count = listBox1.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                //先删集合
                listpath.RemoveAt(listBox1.SelectedIndex);
                //再删列表
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
        //设置静音键
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Tag.ToString() == "1")
            {
                MusicPlayer.settings.mute = true;
                button5.Image = Image.FromFile(@"C:\Users\hp\Desktop\c#学习\C#winform窗体运用程序\音乐播放器\资源\静音.jpg");
                button5.Tag = "2";
            }
            else if (button5.Tag.ToString() == "2")
            {
                MusicPlayer.settings.mute = false;
                button5.Image = Image.FromFile(@"C:\Users\hp\Desktop\c#学习\C#winform窗体运用程序\音乐播放器\资源\放音.jpg");
                button5.Tag = "1";
            }
        }
        //增大音量
        private void button7_Click(object sender, EventArgs e)
        {
            MusicPlayer.settings.volume += 5;
        }
        //减小音量
        private void button8_Click(object sender, EventArgs e)
        {
            MusicPlayer.settings.volume -= 5;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double t1 = double.Parse(MusicPlayer.currentMedia.duration.ToString());
            double t2 = double.Parse(MusicPlayer.Ctlcontrols.currentPosition.ToString()) + 1;
            //如果播放器正处于播放状态
            if (MusicPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                label1.Text = MusicPlayer.currentMedia.duration.ToString("0.00");
                label2.Text = MusicPlayer.Ctlcontrols.currentPosition.ToString("0.00");
                if (t1 <= t2)
                {
                    int index = listBox1.SelectedIndex;
                    //清空已经选中的索引
                    listBox1.SelectedIndices.Clear();
                    index++;
                    //播放到最后一曲时回到第一曲
                    if (index == listBox1.Items.Count)
                    {
                        index = 0;
                    }
                    //将当前索引赋值给选中的索引
                    listBox1.SelectedIndex = index;
                    MusicPlayer.URL = listpath[index];
                    MusicPlayer.Ctlcontrols.play();
                }
            }
        }
        #region 设置渐变色窗体
        //添加窗体的Paint事件，用颜色填充窗体区域：添加引用：using System.Drawing.Drawing2D;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        //    Graphics g = e.Graphics;
        //    Color FColor = Color.Blue;
        //    Color TColor = Color.Yellow;


        //    Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.ForwardDiagonal);


        //    g.FillRectangle(b, this.ClientRectangle);
        }
        ////当改变窗体大小的时候，应该重绘制窗体，添加Resize事件：
        private void Form1_Resize(object sender, EventArgs e)
        {
        //    this.Invalidate();//重绘窗体
        }
        #endregion
    }
}
