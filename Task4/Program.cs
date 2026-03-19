using System;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Vdovy\source\repos\lab3KPZ\Task4\test.txt";

            ISmartTextReader reader = new SmartTextReader();
            ISmartTextReader checker = new SmartTextChecker(new SmartTextReader());
            ISmartTextReader locker = new SmartTextReaderLocker(new SmartTextReader(), ".*secret.*");

            var data1 = reader.Read(path);
            var data2 = checker.Read(path);
            var data3 = locker.Read(path);
        }
    }
}