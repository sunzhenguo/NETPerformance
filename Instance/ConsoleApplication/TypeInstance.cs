using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication
{

    /**
     * 测试 类型的实例化的性能
     * 
     * **/


    class TypeInstance
    {

        public static void Start()
        {
            int times = 1000000;

            // 空调用
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < times; i++) { }
            watch.Stop();
            Console.WriteLine("1000000 times loop " + watch.ElapsedMilliseconds + "ms");


            //直接实例化并调用
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++) 
            {
                var sample1 = new TypeInstance();
                sample1.Call();
            }
            watch.Stop();
            Console.WriteLine("1000000 times Directly invoke " + watch.ElapsedMilliseconds + "ms");


            //Activator.CreateInstance + dynamic 后调用
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++)
            {
                Type t = typeof(TypeInstance);

                dynamic dt = Activator.CreateInstance(t);


//                dt.Call();

            }
            watch.Stop();
            Console.WriteLine("1000000 times Activator CreateInstance " + watch.ElapsedMilliseconds + "ms");

        }



        public void Call()
        {

        }
    }
}
