using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

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
            Console.WriteLine(string.Format("{0} times only loop: {1} ms", times.ToString(), watch.ElapsedMilliseconds));



            //直接实例化并调用
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++) 
            {
                var sample1 = new TypeInstance();
                sample1.Call();
            }
            watch.Stop();
            Console.WriteLine(string.Format("{0} times Directly invoke: {1} ms", times.ToString(), watch.ElapsedMilliseconds));


            //Activator.CreateInstance + dynamic 后调用
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++)
            {
                Type t = typeof(TypeInstance);

                dynamic dt = Activator.CreateInstance(t);


                dt.Call();

            }
            watch.Stop();
            Console.WriteLine(string.Format("{0} times Activator CreateInstance: {1} ms", times.ToString(), watch.ElapsedMilliseconds));

            // 
            watch.Reset();
            watch.Start();
            for (int i = 0; i < times; i++)
            {
                Type t = typeof(TypeInstance);

                TypeInstance widgetFactory =
               (TypeInstance) FormatterServices.GetUninitializedObject(t);
                TypeInstance dt = widgetFactory.CreateInit<TypeInstance>();
                dt.Call();
            }
            watch.Stop();
            Console.WriteLine(string.Format("{0} times FormatterServices: {1} ms", times.ToString(), watch.ElapsedMilliseconds));



        }



        public void Call()
        {

        }


        public T CreateInit<T>() where T:new()
        {
            return new T();
        }
    }
}
