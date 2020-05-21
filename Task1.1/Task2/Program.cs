using Common;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<ShiftedLeftPiramidHandler>()
                   .StartHandle();
        }
    }
}
