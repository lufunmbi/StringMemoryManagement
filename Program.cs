using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;

namespace StringMemoryManagement
{
    class Program
    {

        static void Main(string[] args)
        {


            BenchmarkRunner.Run<benchy>();


        }
    }
    [MemoryDiagnoser]
    public class benchy
    {
        private const string password = "Password123$";

        [Benchmark]
        public string MaskNaive()
        {

            var firstChars = password.Substring(0, 3);
            var length = password.Length - 3;

            for (var i = 0; i < length; i++)
            {
                firstChars += '*';
            }

            return firstChars;
        }

        [Benchmark]
        public string MaskStringBuilder()
        {

            var firstChars = password.Substring(0, 3);
            var length = password.Length - 3;
            var stringbuilder = new StringBuilder(firstChars);

            for (var i = 0; i < length; i++)
            {
                stringbuilder.Append('*');
            }

            return stringbuilder.ToString();
        }

        [Benchmark]
        public string MaskNewString()
        {

            var firstChars = password.Substring(0, 3);
            var length = password.Length - 3;
            var asterisks = new string('*', length);

            var pass = firstChars + asterisks;

            return pass;
        }


        [Benchmark]
        public string MaskStringCreate()
        {
            return string.Create(password.Length, password, (span, value) =>
            {
                value.AsSpan().CopyTo(span);
                span[3..].Fill('*');
            });

        }
    }
}
