using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;

namespace BestWayToManageString
{
    class Program
    {
        /*
         * Mutable and immutable are English words that mean "can change" and "cannot change" respectively. The meaning of these words is the same in C# programming language; that means the mutable types are those whose data members can be changed after the instance is created but Immutable types are those whose data members can not be changed after the instance is created.
         * When we change the value of mutable objects, value is changed in same memory. But in immutable type, the new memory is created and the modified value is stored in new memory.
         */


        static void Main(string[] args)
        {

            BenchmarkRunner.Run<BanchMarkMonitor>();
        }
    }

    [MemoryDiagnoser]
    public class BanchMarkMonitor
    {
        public const string myString = "This is my test string";

        [Benchmark]
        public string Common1()
        {
            var firstChar = myString.Substring(0, 4);
            var length = myString.Length - 4;

            for (int i = 0; i < length; i++)
            {
                firstChar = firstChar += "*";
            }

            return firstChar;
        }


        [Benchmark]
        public string Common2StringBuilder()
        {
            var firstChar = myString.Substring(0, 4);
            var length = myString.Length - 4;
            var stringBuilder = new StringBuilder(firstChar);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append("*");
            }

            return stringBuilder.ToString();
        }


        [Benchmark]
        public string Common3StringClass()
        {
            var firstChar = myString.Substring(0, 4);
            var length = myString.Length - 4;
            var result = new string('*', length);

            return firstChar + result;
        }

        [Benchmark]
        public string Common4StringCreate()
        {
            return string.Create(myString.Length, myString, (span, valuue) => 
            {

                valuue.AsSpan().CopyTo(span);
                span[4..].Fill('*');
            });
        }

    }

}
