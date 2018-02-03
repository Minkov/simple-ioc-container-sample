using System;
using System.Linq;
using SimpleIoCContainer.Demo.Commmands.Contracts;

namespace SimpleIoCContainer.Demo.Commmands
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
