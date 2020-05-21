using Common;

namespace Task4
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<TextRepairHandler>()
                   .StartHandle();
        }
    }
}
