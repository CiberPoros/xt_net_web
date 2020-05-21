using Common;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<ChangeFontHandler>()
                   .StartHandle();
        }
    }
}
