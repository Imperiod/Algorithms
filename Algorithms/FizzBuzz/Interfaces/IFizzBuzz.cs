using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.FizzBuzz.Interfaces
{
    public interface IFizzBuzz
    {
        public List<string> GenList(ulong up_To, IFizzBuzzRulesCompositer compositer);
    }
}
