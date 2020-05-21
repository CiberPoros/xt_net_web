using Common;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<PositiveNumbersChangeHandler>()
                   .StartHandle();
        }
    }
}
