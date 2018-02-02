namespace SimpleDiContainer.Demo
{
    interface ICommand<T>
    {
        void Execute(params T[] args);
    }
}
