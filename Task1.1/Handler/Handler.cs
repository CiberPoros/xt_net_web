using System;

namespace Common
{
    public abstract class Handler
    {
        protected virtual string StartInfo => "Write input data...";
        protected virtual string ResultInfo => "Result:";
        protected virtual bool MakePause => false;

        public void StartHandle()
        {
            for (; ; )
            {
                WriteLine(StartInfo);

                var data = ReadData();
                try
                {
                    string resultHadnle = HandleData(data);

                    WriteLine(ResultInfo);
                    WriteLine(resultHadnle);
                }
                catch (ArgumentException)
                {
                    WriteLine($"Incorrect input data. Try again.");
                }
                finally
                {
                    WriteLine();

                    if (MakePause)
                        Pause();
                }
            }
        }

        protected virtual string ReadData() => ReadLine();

        protected abstract string HandleData(string data);

        private string ReadLine() => Console.ReadLine();

        private void WriteLine(string data) => Console.WriteLine(data);

        private void WriteLine() => WriteLine("");

        private void Pause()
        {
            WriteLine("Press any key to continue...");
            ReadLine();
        }
    }
}
