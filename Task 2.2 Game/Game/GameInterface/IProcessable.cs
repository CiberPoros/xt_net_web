namespace GameInterface
{
    public interface IProcessable
    {
        void Process();

        int Proirity { get; }
    }
}
