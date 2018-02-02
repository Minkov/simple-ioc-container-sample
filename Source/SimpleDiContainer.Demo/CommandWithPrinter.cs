using System.Linq;

namespace SimpleDiContainer.Demo
{
    class CommandWithPrinter<T> : ICommand<T>
    {
        private IPrinter printer;

        public CommandWithPrinter(IPrinter printer) => this.printer = printer;

        void ICommand<T>.Execute(params T[] args)
        {
            args.ToList()
                .ForEach(x => this.printer.Print(x.ToString()));
        }
    }
}
