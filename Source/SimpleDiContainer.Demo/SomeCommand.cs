using System;
using System.Linq;

namespace SimpleDiContainer.Demo
{
    class SomeCommand<T> : ICommand<T>
    {
        void ICommand<T>.Execute(params T[] args)
        {
            Console.WriteLine(" --- Some Command ---");
            args.ToList()
                .ForEach((obj) => Console.WriteLine(obj));
        }
    }
}
