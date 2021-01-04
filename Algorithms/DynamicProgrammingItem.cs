using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class DynamicProgrammingItem
    {
        public string Name { get; }

        public int Value { get; }

        public int Weight { get; }

        public DynamicProgrammingItem(string name, int weight, int value)
        {
            Name = name;
            Value = value;
            Weight = weight;
        }
    }
}
