using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Algorithms
{
    public class SortNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }

        //private int Length { get; set; } = 1;

        SortNode<T> NextNode { get; set; } = null;



        public SortNode(T[] values)
        {
            Value = values[0];

            if (values.Length > 1)
            {
                NextNode = new SortNode<T>(values[1..]);

                if (Value.CompareTo(NextNode.Value) != -1)
                {
                    AddNode(Value);
                    Value = NextNode.Value;
                    NextNode = NextNode.NextNode;
                }
            }
            if(values.Length > 5000)
                Console.WriteLine(values.Length);
        }



        public void AddNode(T value)
        {
            if (NextNode is null)
                NextNode = new SortNode<T>(new [] { value });
            else
            {
                if (value.CompareTo(NextNode.Value) > 0)
                    NextNode.AddNode(value);
                else
                {
                    T[] list = new [] { value };
                    NextNode = new SortNode<T>(list.Concat(NextNode.ToArray()).ToArray());
                }
            }
        }

        public override string ToString() => NextNode is null ? $"[{Value}]" : $"[{Value}] -> " + NextNode.ToString();

        public List<T> ToList()
        {
            List<T> resultArray = new List<T>();

            resultArray.Add(Value);

            if (NextNode != null)
                resultArray.AddRange(NextNode.ToList());

            return resultArray;
        }

        public T[] ToArray() => ToList().ToArray();
    }
}
