using Common;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<XMasTreeHandler>()
                   .StartHandle();
        }
    }
}
