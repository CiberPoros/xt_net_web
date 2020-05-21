using Common;

namespace Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<EvenElementsArrayHandler>()
                   .StartHandle();
        }
    }
}
