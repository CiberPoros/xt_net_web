using Common;

namespace Task6
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<ChangeFontHandler>()
                   .StartHandle();
        }
    }
}
