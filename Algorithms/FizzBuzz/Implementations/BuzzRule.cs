using Algorithms.FizzBuzz.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.FizzBuzz.Implementations
{
    public class BuzzRule : IFizzBuzzRule
    {
        public byte Priority { get; init; }

        public BuzzRule(byte priority)
        {
            Priority = priority;
        }

        public bool Feet(ulong number)
        {
            return number % 5 == 0;
        }

        public string GetReplacement()
        {
            return "Buzz";
        }

        public int Compare(IFizzBuzzRule x, IFizzBuzzRule y)
        {
            if (x.Priority < y.Priority)
            {
                return -1;
            }
            else if(x.Priority > y.Priority)
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
