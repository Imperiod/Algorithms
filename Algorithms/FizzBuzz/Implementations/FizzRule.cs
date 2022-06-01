using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.FizzBuzz.Interfaces;

namespace Algorithms.FizzBuzz.Implementations
{
    public class FizzRule : IFizzBuzzRule
    {
        public byte Priority { get; init; }

        public FizzRule(byte priotity)
        {
            Priority = priotity;
        }

        public bool Feet(ulong number)
        {
            return number % 3 == 0;
        }

        public string GetReplacement()
        {
            return "Fizz";
        }

        public int Compare(IFizzBuzzRule x, IFizzBuzzRule y)
        {
            if (x.Priority < y.Priority)
            {
                return -1;
            }
            else if (x.Priority > y.Priority)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
