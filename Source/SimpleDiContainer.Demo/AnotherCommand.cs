using System;
using System.Linq;

namespace SimpleDiContainer.Demo
{
    class AnotherCommand<T> : ICommand<T>
    {
        void ICommand<T>.Execute(params T[] args)
        {
            Console.WriteLine(" --- Another Command ---");
            args.ToList()
                .ForEach((obj) => Console.WriteLine(obj));
        }
    }
}
