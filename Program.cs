using System;
using System.Threading;

namespace helloworlddocker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool always = true;
            while (always == true) {
                Console.WriteLine("Hello World, Windows (dauernd from k3s 4) !!!");
                Thread.Sleep(10000);
            }
            

        }
    }
}
