using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.FizzBuzz.Interfaces
{
    public interface IFizzBuzzRule : IComparer<IFizzBuzzRule>
    {
        public bool Feet(ulong number);

        public string GetReplacement();

        public byte Priority { get; }
    }
}
