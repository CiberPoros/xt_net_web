using Common;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<AverageLengthHandler>()
                   .StartHandle();
        }
    }
}
