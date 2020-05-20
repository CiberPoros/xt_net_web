using System;

namespace Common
{
    public abstract class Handler
    {
        protected virtual string StartInfo => "Write input data...";
        protected virtual string ResultInfo => "Result:";

        public void StartHandle()
        {
            for (; ; )
            {
                WriteLine(StartInfo);

                var data = ReadLine();
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
                }
            }
        }

        private string ReadLine() => Console.ReadLine();

        private void WriteLine(string data) => Console.WriteLine(data);

        private void WriteLine() => this.WriteLine("");

        protected abstract string HandleData(string data);
    }
}
