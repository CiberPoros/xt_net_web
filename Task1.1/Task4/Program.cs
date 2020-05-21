using Common;

namespace Task4
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<XMasTreeHandler>()
                   .StartHandle();
        }
    }
}
