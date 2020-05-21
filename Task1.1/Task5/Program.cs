using Common;

namespace Task5
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<ResuidesSumHandler>()
                   .StartHandle();
        }
    }
}
