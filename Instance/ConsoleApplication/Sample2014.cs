/**
 *  发现一个问题， 在 DynamicMethod 这个测试程序中的直接调用方法和反射调用方法的差距是100多倍，
 *  而在 FastInvokeHandler 中两者的差距只有5，6倍，这是为什么哪
 *  我就利用这个程序来测试一下。
 * 
 * **/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ConsoleApplication
{



    class Sample2014
    {

        public static void Start()
        {
            int times = 1000000; //循环一百万次

            //初始化


            Person person = new Person();

            string word = "hello";
            Person p = null;
            object[] param = new object[] { word, p, 3 };


            //测试直接调用类的方法使用的时间

            Stopwatch watch1 = new Stopwatch();
            watch1.Start();
            for (int i = 0; i < times; i++)
            {
                person.Say(ref word, out p, 3);
            }
            watch1.Stop();
            Console.WriteLine(watch1.Elapsed + " (Directly invoke)");



            //测试MethodInfo.Invoke 方法使用的时间

            Type t = typeof(Person);
            MethodInfo methodInfo = t.GetMethod("Say");

            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            for (int i = 0; i < times; i++)
            {
                methodInfo.Invoke(person, param);
            }
            watch2.Stop();
            Console.WriteLine(watch2.Elapsed + " (Reflection invoke)");


        }


    }


}
