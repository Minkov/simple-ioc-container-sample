using System;
using SimpleIoCContainer.Demo.Commmands;
using SimpleIoCContainer.Demo.Commmands.Contracts;
using SimpleIoCContainer.Lib;

namespace SimpleIoCContainer.Demo
{

    class MainClass
    {
        public static void Main()
        {
            var container = new SimpleContainer();

            container.RegisterSingleton<IPrinter, ConsolePrinter>()
                     .Register<ICommand<string>, SomeCommand<string>>()
                     .Register<ICommand<int>, PrintCommand<int>>();

            var intCommand = container.Resolve<ICommand<int>>();
            var strCommand = container.Resolve<ICommand<string>>();

            strCommand.Execute("One", "Two", "Three");
            intCommand.Execute(1, 2, 3);

            IPrinter printer1 = container.Resolve<IPrinter>();
            IPrinter printer2 = container.Resolve<IPrinter>();
            Console.WriteLine(printer1 == printer2);

            ICommand<int> command1 = container.Resolve<ICommand<int>>();
            ICommand<int> command2 = container.Resolve<ICommand<int>>();
            Console.WriteLine(command1 == command2);
        }

        public static void Test<T>()
        {
            Console.WriteLine(typeof(ICommand<T>));
            Console.WriteLine(typeof(ICommand<int>));
            Console.WriteLine(typeof(ICommand<T>).Equals(typeof(ICommand<int>)));

        }
    }
}
