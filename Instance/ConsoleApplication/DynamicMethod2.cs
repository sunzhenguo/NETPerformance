/**
 *  
 * 由于 老赵的代码中调用一个方法使用的时间和 FastInvoke 中调用一个方法是用的时间相差太多，所以在这里
 *  优化一下 FastInvoke 的代码。
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



    class ClassDynamicMethod2
    {

        public static void Start()
        {

            Console.WriteLine("DynamicMethodExecutor2");
            int times = 1000000; //循环一百万次

            //初始化
            ClassDynamicMethod2_1 program = new ClassDynamicMethod2_1();

            object[] parameters = new object[] { new object(), new object(), new object() };
            program.Say(null, null, null); // force JIT-compile


            //测试直接调用类的方法使用的时间

            Stopwatch watch1 = new Stopwatch();
            watch1.Start();
            for (int i = 0; i < times; i++)
            {
                program.Say(parameters[0], parameters[1], parameters[2]);
            }
            watch1.Stop();
            Console.WriteLine(string.Format("{0} times invoked by DirectCall: {1} ms", times.ToString(), watch1.ElapsedMilliseconds));



            //测试MethodInfo.Invoke 方法使用的时间

            MethodInfo methodInfo = typeof(ClassDynamicMethod2_1).GetMethod("Say");
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            for (int i = 0; i < times; i++)
            {
                methodInfo.Invoke(program, parameters);
            }
            watch2.Stop();
            Console.WriteLine(string.Format("{0} times invoked by Reflection: {1} ms", times.ToString(), watch2.ElapsedMilliseconds));



            //老赵的表达式树

            DynamicMethodExecutor executor = new DynamicMethodExecutor(methodInfo);
            Stopwatch watch3 = new Stopwatch();
            watch3.Start();
            for (int i = 0; i < times; i++)
            {
                executor.Execute(program, parameters);
            }
            watch3.Stop();
            Console.WriteLine(string.Format("{0} times invoked by Dynamic executor: {1} ms", times.ToString(), watch3.ElapsedMilliseconds));


        }



    }


    public class ClassDynamicMethod2_1
    {
        public void Say(object o1, object o2, object o3) { }
    }


}
