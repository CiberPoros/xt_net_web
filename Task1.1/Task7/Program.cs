using Common;

namespace Task7
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<ArrayFuncHandler>()
                   .StartHandle();
        }
    }
}
