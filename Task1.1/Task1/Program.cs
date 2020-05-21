using Common;

namespace Task1
{
    internal class Program
    {
        static void Main()
        {
            Handler.CreateInstance<RectangleAreaHandler>()
                   .StartHandle();
        }
    }
}
