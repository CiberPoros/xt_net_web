using Common;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<CentralPiramidHandler>()
                   .StartHandle();
        }
    }
}
