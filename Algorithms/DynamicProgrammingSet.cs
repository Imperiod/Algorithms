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
        public List<DynamicProgrammingItem> Items { get; }

        private Dictionary<decimal, List<DynamicProgrammingItem>> PreviousItems { get; set; }

        public decimal MaxWeight
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

        private decimal maxWeight = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="list">List DynamicProgrammingItems</param>
        /// <param name="maxWeight">Only positive and must be great than min weight in list items if list is not empty</param>
        public DynamicProgrammingSet(List<DynamicProgrammingItem> list = null, decimal maxWeight = 0)
        {
            Items = list is null ? new List<DynamicProgrammingItem>() : list;
            Items.Sort();

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

            CheckData();

            PreviousItems = new Dictionary<decimal, List<DynamicProgrammingItem>>();
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
        public List<DynamicProgrammingItem> GetMaxItems() => GetMaxItemsByWeight();

        /// <summary>
        /// Use to find items with highest value with a given bounding weight.
        /// </summary>
        /// <param name="list">Usually not used because internal elements are used instead of a list. Use only if you need a different Dynamic Programming Items collection.</param>
        /// <param name="remainder">Must be great than min weight in list items.</param>
        /// <param name="totalValue">Never use.</param>
        /// <param name="topFunc">Never use.</param>
        /// <returns>Items with highest value with a given bounding weight.</returns>
        public List<DynamicProgrammingItem> GetMaxItemsByWeight(List<DynamicProgrammingItem> list = null, decimal remainder = 0, decimal totalValue = 0, bool topFunc = true)
        {
            #region Protectors
            //Protector
            if (remainder == 0)
                remainder = MaxWeight;

            //Protector
            if (list is null || list.Count == 0)
                list = Items?.Where(w => w?.Weight <= remainder)?.ToList();
            else
                list = list.FirstOrDefault(f => f.Weight > remainder) is null ? list : list.Where(w => w.Weight <= remainder).ToList();

            //Protector
            if (list.Count == 0 || list is null)
                throw new Exception("No data, use constructor or method AddItem");
            #endregion

            //Optimizer
            if (topFunc && PreviousItems.ContainsKey(remainder))
                return PreviousItems[remainder];
            
            List<DynamicProgrammingItem> result = new List<DynamicProgrammingItem>();
            List<DynamicProgrammingItem> previousResult = new List<DynamicProgrammingItem>();

            decimal minWeight = list.Min(m => m.Weight);

            for (int i = 0; i < list.Count; i++)
            { 
                //Optimizer
                if (remainder < minWeight)
                    break;

                if (remainder - list[i].Weight >= 0)
                {
                    result.Add(list[i]);
                    remainder -= list[i].Weight;
                }
            }

            previousResult.AddRange(result);

            for (int i = 0; i < previousResult.Count; i++)
            {
                decimal preRemainder = remainder + previousResult[i].Weight;

                //Protector
                if (result.Contains(previousResult[i]) == false)
                    continue;

                List<DynamicProgrammingItem> innerList = new List<DynamicProgrammingItem>();

                innerList.AddRange(list.Where(w => result.Contains(w) == false && w.Weight <= preRemainder));

                if (innerList.Count == 0)
                    break;
                else
                {
                    List<DynamicProgrammingItem> innerResult = GetMaxItemsByWeight(innerList, preRemainder, result.Sum(s => s.Value), false);
                    if (innerResult.Sum(s => s.Value) > previousResult[i].Value)
                    {
                        result.Remove(previousResult[i]);
                        result.AddRange(innerResult);
                    }
                }
            }

            //Optimizer
            if (topFunc && PreviousItems.ContainsKey(remainder + result.Sum(s => s.Weight)) == false)
                PreviousItems.Add(remainder + result.Sum(s => s.Weight), result);

            return result;
        }

        /// <summary>
        /// Used to get the list of items with the highest value for a given maximum weight as a string.
        /// </summary>
        /// <returns>List of items with the highest value for a given maximum weight.</returns>
        public string ToStringMaxItems()
        {
            CheckData();

            List<DynamicProgrammingItem> items = GetMaxItems();

            //Protector
            if (items is null || items.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            StringBuilder stringBuilder = new StringBuilder();

            items.ForEach(f => stringBuilder.Append($"{f}\n"));

            stringBuilder.Append("==========================================\n");
            stringBuilder.Append($"Total => [{ items.Sum(s => s.Weight)} : { items.Sum(s => s.Value)}] R: { MaxWeight - items.Sum(s => s.Weight)}\n");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Use to get a list of the items with the highest value for a given bounding weight as a string.
        /// </summary>
        /// <param name="weight">Boundary weight.</param>
        /// <returns>List of the items with the highest value for a given bounding weight.</returns>
        public string ToStringMaxItemsByWeight(decimal weight)
        {
            CheckData();

            List<DynamicProgrammingItem> items = GetMaxItemsByWeight(remainder: weight);

            //Protector
            if (items is null || items.Count == 0)
                throw new Exception("No data, use constructor or method AddItem");

            StringBuilder stringBuilder = new StringBuilder();

            items.ForEach(f => stringBuilder.Append($"{f}\n"));
            stringBuilder.Append("==========================================\n");
            stringBuilder.Append($"Total => [{ items.Sum(s => s.Weight)} : { items.Sum(s => s.Value)}] R: { weight - items.Sum(s => s.Weight)}\n");

            return stringBuilder.ToString();
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

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Collection consist of:\n");
            Items.ForEach(f => stringBuilder.Append($"{f}\n"));
            stringBuilder.Append("==========================================\n");
            stringBuilder.Append($"Total => [{ Items.Sum(s => s.Weight)} : { Items.Sum(s => s.Value)}] Max weight: {MaxWeight}\n");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Protector which check data like MaxWeight
        /// </summary>
        void CheckData()
        {
            //Protector
            if (MaxWeight < 0)
                throw new ArgumentException("Weight can be only positive", nameof(MaxWeight));

            //Protector
            if (Items != null && Items.Count > 0 && MaxWeight < Items.Min(m => m.Weight))
                throw new ArgumentException("MaxWeight must be equal or great than min weight in list items", nameof(MaxWeight));
        }
    }
}
