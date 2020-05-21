using Common;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<RectangleAreaHandler>()
                   .StartHandle();
        }
    }
}
