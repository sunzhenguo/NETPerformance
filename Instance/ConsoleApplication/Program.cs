using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeInstance.Start();

            Console.WriteLine("测试完成，按任意键退出");
            Console.Read();
        }
    }
}
