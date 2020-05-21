using Common;

namespace Task10
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<EvenElementsArrayHandler>()
                   .StartHandle();
        }
    }
}
