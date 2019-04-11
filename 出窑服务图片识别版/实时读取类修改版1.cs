using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 出窑服务图片识别版
{
    class 实时读取类修改版1
    {
        private string 取流路径 = ConfigurationManager.AppSettings["取流路径"];
        private string 临时图片路径 = ConfigurationManager.AppSettings["临时图片路径"];
        private string 显示小窗标识 = ConfigurationManager.AppSettings["显示小窗标识"];

        private string 显示实时影像标识 = ConfigurationManager.AppSettings["显示实时影像标识"];




        private string 等待时间 = ConfigurationManager.AppSettings["等待时间"];
        private string 重启路径 = ConfigurationManager.AppSettings["重启路径"];
        private string 大于多少得概率 = ConfigurationManager.AppSettings["大于多少得概率"];
        public delegate void 错误日记委托(string str, Exception ex);

        internal void 开始实时取()
        {
            throw new NotImplementedException();
        }

        public event 错误日记委托 错误日记事件;
        protected virtual void On错误日记事件触发(string str, Exception ex)
        {
            if (错误日记事件 != null)
            {
                错误日记事件(str, ex); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        VideoCapture capture;
        图片识别类 _图片识别类;
        逻辑处理类 _逻辑处理类;
        数据处理类 _数据处理类;
        错误日记类 _错误日记类;
        SQL操作类 _SQL操作类;
        SQLite操作类 _SQLite操作类;
        重启类 _重启类;

        private Mat frame;
        private Mat frame备份;

        bool 开始标识;

        public 实时读取类修改版1()
        {
            _重启类 = new 重启类(重启路径);
            _逻辑处理类 = new 逻辑处理类();
            _数据处理类 = new 数据处理类();
            _错误日记类 = new 错误日记类();
            _SQL操作类 = new SQL操作类();
            _SQLite操作类 = new SQLite操作类();


            frame = new Mat();
            frame备份 = new Mat();
            InitEvent();

            try
            {
                capture = new VideoCapture(取流路径);


            }
            catch (Exception ex)
            {
                On错误日记事件触发("取流错误", ex);
                _重启类.重启(重启路径);
            }

            try
            {
                _图片识别类 = new 图片识别类();
            }
            catch (Exception ex)
            {
                On错误日记事件触发("图片识别类", ex);
                _重启类.重启(重启路径);
            }
        }

        private void InitEvent()
        {
            //逻辑处理类.重启事件 += _重启类.重启;

            错误日记事件 += _错误日记类.错误日记执行函数;

            //逻辑处理类.采集事件 += _出窑工位判断类.工位判断执行类;

            _逻辑处理类.一号生产线下车事件 += _数据处理类.一号生产线下车事件执行函数;
            _逻辑处理类.二号生产线下车事件 += _数据处理类.二号生产线下车事件执行函数;
            _逻辑处理类.出一号窑事件 += _数据处理类.出一号窑事件执行函数;
            _逻辑处理类.出二号窑事件 += _数据处理类.出二号窑事件执行函数;
            _逻辑处理类.出三号窑事件 += _数据处理类.出三号窑事件执行函数;
            _逻辑处理类.出四号窑事件 += _数据处理类.出四号窑事件执行函数;

            _逻辑处理类.满车事件 += _数据处理类.满车事件执行函数;
            _逻辑处理类.空车事件 += _数据处理类.空车事件执行函数;

            _逻辑处理类.保存变化值事件 += _数据处理类.进出窑_变化日记;
            _逻辑处理类.工位判断错误日记事件 += _错误日记类.错误日记执行函数;

            _数据处理类.插入SQL事件 += _SQL操作类.SQL操作事件执行函数;
            _数据处理类.插入SQLite事件 += _SQLite操作类.SQL操作事件执行函数;
            _数据处理类.插入SQLite错误日记事件 += _错误日记类.错误日记执行函数;
            _数据处理类.插入SQL错误事件 += _错误日记类.错误日记执行函数;
        }


        public bool 开始实时取流标识1 { get => 开始标识; set => 开始标识 = value; }
        public bool 开始实时识别标识1 { get; set; }

        private static object objlock = new object();

        public void 开始实时取流()
        {
            using (var window实时图像 = new Window("实时图像"))
            {
                int II = 0;
                Random rd = new Random();
                while (true)
                {
                    while (开始实时取流标识1)
                    {
                        try
                        {


                            lock (objlock)
                            {
                                capture.Read(frame);
                            }


                            if (frame.Empty())
                            {
                                capture = new VideoCapture(取流路径);
                                break;
                            }



                            if (显示实时影像标识 == "1")
                            {
                                window实时图像.ShowImage(frame);
                            }
                            Thread.Sleep(39);
                            Cv2.WaitKey(1);


                        }
                        catch (Exception)
                        {

                            throw;
                        }


                    }

                }
            }

        }
        private static object objlock1 = new object();

        public void 开始实时识别()
        {
            float ou = 0;
            Int32 I = 0;

            float ou1 = 0;
            Int32 I1 = 0;
            Point textPos = new Point(1, 100);
            Point textPos1 = new Point(1, 120);


            while (true)
            {
                while (开始实时识别标识1)
                {
                    using (var window概率 = new Window("概率"))
                    using (var image缩小 = new Mat())
                    {
                        lock (objlock1)
                        {
                            frame备份 = frame.Clone();
                        }

                        //if (frame备份)
                        //{

                        //}

                        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                        stopwatch.Start(); //  开始监视代码
                        try
                        {
                            Cv2.Resize(frame备份, image缩小, new Size(208, 208), 0, 0, InterpolationFlags.Linear); //缩小28*28

                            Cv2.ImWrite(临时图片路径, image缩小);
                            //byte[] bytes = image缩小.ImEncode(".jpg");

                            try
                            {
                                I = _图片识别类.识别方法(out ou);

                                //I1 = _图片识别类.识别方法(out ou1, bytes);


                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                            //I = rd.Next(100, 500);
                            //ou = 0.5;
                            if (ou > Convert.ToDouble(大于多少得概率))
                            {
                                _逻辑处理类.逻辑判断方法(I);
                            }

                            stopwatch.Stop(); //  停止监视
                            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
                                                                   //double hours = timeSpan.TotalHours; // 小时
                                                                   //double minutes = timeSpan.TotalMinutes;  // 分钟
                                                                   //double seconds = timeSpan.TotalSeconds;  //  秒数
                            double milliseconds = timeSpan.TotalMilliseconds;  //  毫秒数

                            if (显示小窗标识 == "1")
                            {
                                image缩小.PutText(I.ToString() + "    " + ou.ToString() + "  " + milliseconds.ToString(), textPos,
                                    HersheyFonts.HersheySimplex, 0.5, Scalar.White);
                                image缩小.PutText(I1.ToString() + "    " + ou1.ToString() + "  " + milliseconds.ToString(), textPos1,
                                    HersheyFonts.HersheySimplex, 0.5, Scalar.White);
                                window概率.ShowImage(image缩小);

                            }

                            if ((int)milliseconds > 1000)
                            {
                                Cv2.WaitKey(1);
                            }
                            else
                            {

                                Thread.Sleep(1000 - (int)milliseconds);
                                Cv2.WaitKey(1);

                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                       

                       



                    }


                }
            }
        }
    }
}
