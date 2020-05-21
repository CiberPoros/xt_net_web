using Common;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<ResuidesSumHandler>()
                   .StartHandle();
        }
    }
}
