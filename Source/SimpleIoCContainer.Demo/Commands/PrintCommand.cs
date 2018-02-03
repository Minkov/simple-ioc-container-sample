using System.Linq;
using SimpleIoCContainer.Demo.Commmands.Contracts;

namespace SimpleIoCContainer.Demo.Commmands
{
    class PrintCommand<T> : ICommand<T>
    {
        private IPrinter printer;

        public PrintCommand(IPrinter printer) => this.printer = printer;

        void ICommand<T>.Execute(params T[] args)
        {
            args.ToList()
                .ForEach(x => this.printer.Print(x.ToString()));
        }
    }
}
