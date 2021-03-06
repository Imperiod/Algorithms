﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class DynamicProgrammingItem : IComparable<DynamicProgrammingItem>
    {
        public string Name { get; }

        public decimal Value { get; }

        public decimal Weight { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight">Only positive</param>
        /// <param name="value">Only positive</param>
        public DynamicProgrammingItem(string name, decimal weight, decimal value)
        {
            //Protector
            if (weight < 0)
                throw new ArgumentException("Weight can be only positive", nameof(weight));
            //Protector
            if (value < 0)
                throw new ArgumentException("Value can be only positive", nameof(value));

            Name = name;
            Value = value;
            Weight = weight;
        }

        public int CompareTo(DynamicProgrammingItem obj) => Value > obj.Value ? -1 : (Value == obj.Value ? 0 : 1);

        public override string ToString()
        {
            return $"{Name} [ {Weight} : {Value} ]";
        }
    }
}
