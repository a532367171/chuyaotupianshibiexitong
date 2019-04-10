using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 出窑服务图片识别版
{
    public partial class Form1 : Form
    {
        Thread vThread1;
        Thread vThread2;

        实时读取类修改版1 _实时取流类;
        public Form1()
        {
            InitializeComponent();

             _实时取流类 = new 实时读取类修改版1();
             vThread1 = new Thread(new ThreadStart(_实时取流类.开始实时取流));
            vThread2 = new Thread(new ThreadStart(_实时取流类.开始实时识别));

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _实时取流类.开始实时取流标识1 = true;
            vThread1.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _实时取流类.开始实时取流标识1 = false;
            vThread1.Join();
            vThread2.Join();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _实时取流类.开始实时识别标识1 = true;
            vThread2.Start();

        }
    }
}
