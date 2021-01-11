using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace Algorithms
{
    /// <summary>
    /// Use this class to find the highest value for a set of elements with a given bounding weight.
    /// </summary>
    public class DynamicProgrammingSet
    {
        List<DynamicProgrammingItem> Items { get; set; }

        Dictionary<double, List<DynamicProgrammingItem>> MaxItemsByWeight { get; }

        public double MaxWeight
        {
            get
            {
                return maxWeight;
            }
            set
            {
                //Protector
                if (value < 0)
                    throw new ArgumentException("Value can be only positive", nameof(value));

                //Protector
                if (Items != null && Items.Count > 0 && value < Items.Min(m => m.Weight))
                    throw new ArgumentException("When items is not empty, value must be equel or great than min weight in items", nameof(MaxWeight));
                maxWeight = value;
            }
        }

        double maxWeight = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="list">List DynamicProgrammingItems</param>
        /// <param name="maxWeight">Only positive and must be great than min weight in list items if list is not empty</param>
        public DynamicProgrammingSet(List<DynamicProgrammingItem> list = null, double maxWeight = 0)
        {
            Items = list is null ? new List<DynamicProgrammingItem>() : list;
            Items.Sort();

            //Protector
            if (maxWeight < 0)
                throw new ArgumentException("MaxWeight can be only positive", nameof(maxWeight));

            //Define max weight
            if (maxWeight == 0)
            {
                if (list is null)
                {
                    MaxWeight = 0;
                }
                else
                {
                    MaxWeight = Items.Max(m => m.Weight);
                }
            }
            else
            {
                MaxWeight = maxWeight;
            }

            //Protector
            if (Items.Count > 0 && maxWeight < Items.Min(m => m.Weight))
                throw new ArgumentException("MaxWeight must be equal or great than min weight in list items", nameof(maxWeight));

            MaxItemsByWeight = new Dictionary<double, List<DynamicProgrammingItem>>();
        }

        public void AddItem(DynamicProgrammingItem item)
        {
            Items.Add(item);
            Items.Sort();
        }

        /// <summary>
        /// Use to find items with highest value with a max weight.
        /// </summary>
        /// <returns>Items with highest value with a max weight.</returns>
        List<DynamicProgrammingItem> GetMaxItems() => GetMaxItemsByWeight();

        /// <summary>
        /// Use to find items with highest value with a given bounding weight.
        /// </summary>
        /// <param name="list">Usually not used because internal elements are used instead of a list. Use only if you need a different Dynamic Programming Items collection.</param>
        /// <param name="remainder">Must be great than min weight in list items.</param>
        /// <param name="mtv">Never use.</param>
        /// <param name="topFunc">Never use.</param>
        /// <returns>Items with highest value with a given bounding weight.</returns>
        List<DynamicProgrammingItem> GetMaxItemsByWeight(List<DynamicProgrammingItem> list = null, double remainder = 0, double mtv = 0, bool topFunc = true)
        {
            //Protector
            if (list is null || list.Count == 0)
                list = Items;

            //Protector
            if (remainder == 0)
                remainder = MaxWeight;

            //Protector
            if (list.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            //Protector
            if (remainder < list.Min(m => m.Weight))
                throw new ArgumentException("Remainder must be equal or great than min weight in list items", nameof(remainder));

            //Optimizer
            if (MaxItemsByWeight.ContainsKey(remainder) && topFunc)
                return MaxItemsByWeight[remainder];
            

            List<DynamicProgrammingItem> result = new List<DynamicProgrammingItem>();

            foreach (DynamicProgrammingItem item in list)
            {
                if (remainder - item.Weight >= 0)
                {
                    result.Add(item);
                    remainder -= item.Weight;
                }
            }

            mtv = result.Sum(s => s.Value);

            List<DynamicProgrammingItem> resultCopy = new List<DynamicProgrammingItem>();
            resultCopy.AddRange(result);

            foreach (DynamicProgrammingItem item in resultCopy)
            {
                //Protector
                if (result.Contains(item) == false)
                    continue;

                List<DynamicProgrammingItem> innerList = new List<DynamicProgrammingItem>();

                innerList.AddRange(list.Where(w => result.Contains(w) == false && w.Weight <= (remainder + item.Weight)));

                if (innerList.Count == 0)
                {
                    break;
                }
                else if(innerList.Count == 1)
                {
                    if (innerList.First().Value > item.Value)
                    {
                        result.Remove(item);
                        result.Add(innerList.First());
                    }
                }
                else
                {
                    List<DynamicProgrammingItem> innerResult = GetMaxItemsByWeight(innerList, remainder + item.Weight, mtv, false);
                    if (innerResult.Sum(s => s.Value) > item.Value)
                    {
                        mtv = innerResult.Sum(s => s.Value);
                        result.Remove(item);
                        result.AddRange(innerResult);
                    }
                }
            }

            if (topFunc && MaxItemsByWeight.ContainsKey(remainder + result.Sum(s => s.Weight)) == false)
            {
                MaxItemsByWeight.Add(remainder + result.Sum(s => s.Weight), result);
            }

            return result;
        }

        /// <summary>
        /// Used to get the list of items with the highest value for a given maximum weight as a string.
        /// </summary>
        /// <returns>List of items with the highest value for a given maximum weight.</returns>
        public string ToStringMaxItems()
        {
            //Protector
            if (Items != null && Items.Count > 0 && MaxWeight < Items.Min(m => m.Weight))
                throw new ArgumentException("MaxWeight must be equal or great than min weight in list items", nameof(MaxWeight));

            List<DynamicProgrammingItem> items = GetMaxItems();

            //Protector
            if (items is null || items.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            string s = "";

            items.ForEach(f => s += $"[N:{f?.Name} W:{f?.Weight} V:{f?.Value}] + ");

            return $"{s[0..^3]} Result => W:{items.Sum(s => s?.Weight)} V:{items.Sum(s => s?.Value)} R:{MaxWeight - items.Sum(s => s?.Weight)}";
        }

        /// <summary>
        /// Use to get a list of the items with the highest value for a given bounding weight as a string.
        /// </summary>
        /// <param name="weight">Boundary weight.</param>
        /// <returns>List of the items with the highest value for a given bounding weight.</returns>
        public string ToStringMaxItemsByWeight(double weight)
        {
            //Protector
            if (weight < 0)
                throw new ArgumentException("Weight can be only positive", nameof(weight));

            //Protector
            if (Items != null && Items.Count > 0 && weight < Items.Min(m => m.Weight))
                throw new ArgumentException("Weight must be equal or great than min weight in list items", nameof(weight));

            List<DynamicProgrammingItem> items = GetMaxItemsByWeight(remainder: weight);

            //Protector
            if (items is null || items.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            string s = "";

            items.ForEach(f => s += $"[N:{f?.Name} W:{f?.Weight} V:{f?.Value}] + ");

            return $"{s[0..^3]} Result => W:{items.Sum(s => s?.Weight)} V:{items.Sum(s => s?.Value)} R:{weight - items.Sum(s => s?.Weight)}";
        }

        /// <summary>
        /// Use to get a list of items as a string.
        /// </summary>
        /// <returns>Return items like [Name Weight Value] and total weight, total value and remainder for max weight.</returns>
        public override string ToString()
        {
            //Protector
            if (Items is null || Items.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            string s = "Collection consist of: ";

            Items.ForEach(f => s += $"[N:{f?.Name} W:{f?.Weight} V:{f?.Value}] + ");

            return $"{s[0..^3]} Result => Total weight: {Items.Sum(s => s?.Weight)} Total value:{Items.Sum(s => s?.Value)} Remainder:{MaxWeight - Items.Sum(s => s?.Weight)} for Max weight:{MaxWeight}";
        }
    }
}
