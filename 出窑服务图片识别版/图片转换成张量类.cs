using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace 出窑服务图片识别版
{
    class 图片转换成张量类
    {
        TFGraph graph1;

        TFOutput input;
        TFOutput output;
        private TFSession session;

        //TFSession.Runner runner;


        private TFTensor[] normalized;


        private string file = ConfigurationManager.AppSettings["临时图片路径"];

        public 图片转换成张量类()
        {
            graph1 = new TFGraph();

            input = graph1.Placeholder(TFDataType.String);

            //output = graph1.ResizeBilinear(
            //    images: graph1.ExpandDims(
            //        input: graph1.Cast(
            //            graph1.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
            //        dim: graph1.Const(0, "make_batch")),
            //    size: graph1.Const(new int[] { 208, 208 }, "size"));

            //ResizeBilinear: 图像缩放，双线性插值 - 图像中心对齐
            //ExpandDims: 想要维度增加一维
            //Cast:tf.cast 类型转换 函数
            //DecodeJpeg:预处理 尺寸调整 decode_jpeg函数


            output = graph1.ResizeBilinear(
                    images: graph1.ExpandDims(
                    input: graph1.Div(
                       x: graph1.Cast(
                           x: graph1.DecodeJpeg(
                            contents: input,
                            channels: 3),
                          DstT: TFDataType.Float),
                       y: graph1.Const((float)255, "div")),
                    dim: graph1.Const(0, "make_batch")),
            size: graph1.Const(new int[] { 208, 208 }, "size"));



            session = new TFSession(graph1);
            //runner = session.GetRunner();
            //runner.Fetch(output);

        }

        private byte[] contents;
        TFTensor tensor;
        private bool b = true;

        public TFTensor 转换方法()
        {
            //if (b)
            //{
            //    contents = File.ReadAllBytes(file);

            //    // DecodeJpeg uses a scalar String-valued tensor as input.

            //    tensor = TFTensor.CreateString(contents);
            //    b = false;
            //}



            contents = File.ReadAllBytes(file);

            //// DecodeJpeg uses a scalar String-valued tensor as input.

            tensor = TFTensor.CreateString(contents);



            //contents  = null;

            // Construct a graph to normalize the image 归一化

            // Execute that graph to normalize this one image 执行图规范化这个形象
            //using (var session = new TFSession(graph1))
            //{
            //    var normalized = session.Run(
            //        inputs: new[] { input },
            //        inputValues: new[] { tensor },
            //        outputs: new[] { output });
            //    //tensor = null;
            //    return normalized[0];
            //}







            //using (var session = new TFSession(graph1))
            //{
            //var session = new TFSession(graph1);
            //    var runner = session.GetRunner();

            //    runner.AddInput(input, tensor);
            //    runner.Fetch(output);
            //    normalized = runner.Run();

            //    //session.CloseSession();
            //    session.DeleteSession();
            //    runner = null;
            //    //return normalized[0];

            ////}
            //return 1;



            using (var session = new TFSession(graph1))
            {

                try
                {
                    var runner = session.GetRunner();

                    runner.AddInput(input, tensor);
                    runner.Fetch(output);
                    normalized = runner.Run();

                    return normalized[0];
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    session.Dispose();
                    //session.CloseSession();
                    //session.DeleteSession();

                }

            }

            //normalized = session.Run(
            //        inputs: new[] { input },
            //        inputValues: new[] { tensor },
            //        outputs: new[] { output });

            //tensor = null;

            //return normalized[0];





        }

        public TFTensor 转换方法(byte[] bytes)
        {
            //if (b)
            //{
            //    contents = File.ReadAllBytes(file);

            //    // DecodeJpeg uses a scalar String-valued tensor as input.

            //    tensor = TFTensor.CreateString(contents);
            //    b = false;
            //}




            //// DecodeJpeg uses a scalar String-valued tensor as input.

            tensor = TFTensor.CreateString(bytes);



            //contents  = null;

            // Construct a graph to normalize the image 归一化

            // Execute that graph to normalize this one image 执行图规范化这个形象
            //using (var session = new TFSession(graph1))
            //{
            //    var normalized = session.Run(
            //        inputs: new[] { input },
            //        inputValues: new[] { tensor },
            //        outputs: new[] { output });
            //    //tensor = null;
            //    return normalized[0];
            //}







            //using (var session = new TFSession(graph1))
            //{
            //var session = new TFSession(graph1);
            //    var runner = session.GetRunner();

            //    runner.AddInput(input, tensor);
            //    runner.Fetch(output);
            //    normalized = runner.Run();

            //    //session.CloseSession();
            //    session.DeleteSession();
            //    runner = null;
            //    //return normalized[0];

            ////}
            //return 1;



            using (var session = new TFSession(graph1))
            {

                try
                {
                    var runner = session.GetRunner();

                    runner.AddInput(input, tensor);
                    runner.Fetch(output);
                    normalized = runner.Run();

                    return normalized[0];
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    session.Dispose();
                    //session.CloseSession();
                    //session.DeleteSession();

                }

            }

            //normalized = session.Run(
            //        inputs: new[] { input },
            //        inputValues: new[] { tensor },
            //        outputs: new[] { output });

            //tensor = null;

            //return normalized[0];





        }

    }
}
