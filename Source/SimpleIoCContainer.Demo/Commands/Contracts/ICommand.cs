namespace SimpleIoCContainer.Demo.Commmands.Contracts
{
    interface ICommand<T>
    {
        void Execute(params T[] args);
    }
}
