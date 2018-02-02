using System;
using System.Linq;
using SimpleDiContainer.Lib;

namespace SimpleDiContainer.Demo
{

    class MainClass
    {
        public static void Main()
        {
            var container = new SimpleContainer();

            container.Register<IPrinter, ConsolePrinter>();

            container.Register<ICommand<string>, SomeCommand<string>>();
            container.Register<ICommand<int>, CommandWithPrinter<int>>();

            var intCommand = container.Resolve<ICommand<int>>();
            var strCommand = container.Resolve<ICommand<string>>();

            strCommand.Execute("One", "Two", "Three");
            intCommand.Execute(1, 2, 3);
        }

        public static void Test<T>()
        {
            Console.WriteLine(typeof(ICommand<T>));
            Console.WriteLine(typeof(ICommand<int>));
            Console.WriteLine(typeof(ICommand<T>).Equals(typeof(ICommand<int>)));

        }
    }
}
