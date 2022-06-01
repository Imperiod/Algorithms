using Algorithms.FizzBuzz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.FizzBuzz.Implementations
{
    public class FizzBuzz : IFizzBuzz
    {
        /// <summary>
        /// Generates a list whose elements comply with the rules
        /// </summary>
        /// <param name="up_To">Last element</param>
        /// <param name="rules">Rules must be sorted in right order</param>
        /// <returns>Generated list</returns>
        public List<string> GenList(ulong up_To, IFizzBuzzRulesCompositer compositer)
        {
            List<string> result = new List<string>();

            for (ulong i = 1; i < up_To; i++)
            {
                result.Add(compositer.Compose(i));
            }

            return result;
        }
    }
}
