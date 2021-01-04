using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace Algorithms
{
    public class DynamicProgrammingSet
    {
        DataTable DataTable = new DataTable();

        List<DynamicProgrammingItem> items { get; set; }

        int MaxWeight = 0;

        public DynamicProgrammingSet(List<DynamicProgrammingItem> items = null, int maxWeight = 0)
        {
            this.items = items is null ? new List<DynamicProgrammingItem>() : items;
            if (maxWeight == 0)
            {
                if (items is null)
                {
                    MaxWeight = 0;
                }
                else
                {
                    MaxWeight = items.Max(m => m.Weight);
                }
            }
            else
            {
                MaxWeight = maxWeight;
            }

            if (items != null)
            {
                this.items = this.items.OrderBy(s => s.Weight).ThenBy(b => b.Value).ToList();
                int r = 0;

                for (int i = 0; i < MaxWeight; i++)
                {
                    DataTable.Columns.Add("", typeof(List<DynamicProgrammingItem>));
                }

                foreach (var item in this.items)
                {
                    DataTable.Rows.Add();

                    for (int c = 0; c < DataTable.Columns.Count; c++)
                    {
                        List<DynamicProgrammingItem> preMax = new List<DynamicProgrammingItem>();
                        List<DynamicProgrammingItem> currMax = new List<DynamicProgrammingItem>();
                        List<DynamicProgrammingItem> max = new List<DynamicProgrammingItem>();

                        if (r > 0)
                            preMax.AddRange((List<DynamicProgrammingItem>)DataTable.Rows[r - 1][c]);
                        
                        if(r > 0 && c - item.Weight >= 0)
                            currMax.AddRange((List<DynamicProgrammingItem>)DataTable.Rows[r - 1][c - item.Weight]);

                        if (c - (item.Weight-1) >= 0)
                            currMax.Add(item);
                        

                        if (preMax.Count == 0)
                            max = currMax.Count != 0 ? currMax : max;
                        else
                            max = preMax.Sum(s => s.Value) > currMax.Sum(s => s.Value) ? preMax : currMax;

                        DataTable.Rows[r][c] = max;
                    }

                    r++;
                }
            }
        }

        public void AddItem(DynamicProgrammingItem item)
        {
            DataTable.Rows.Add();
            int r = DataTable.Rows.Count - 1;

            for (int c = 0; c < DataTable.Columns.Count; c++)
            {
                List<DynamicProgrammingItem> preMax = new List<DynamicProgrammingItem>();
                List<DynamicProgrammingItem> currMax = new List<DynamicProgrammingItem>();
                List<DynamicProgrammingItem> max = new List<DynamicProgrammingItem>();

                if (r > 0)
                    preMax.AddRange((List<DynamicProgrammingItem>)DataTable.Rows[r - 1][c]);

                if (r > 0 && c - item.Weight >= 0)
                    currMax.AddRange((List<DynamicProgrammingItem>)DataTable.Rows[r - 1][c - item.Weight]);

                if (c - (item.Weight - 1) >= 0)
                    currMax.Add(item);

                if (preMax.Count == 0)
                    max = currMax.Count != 0 ? currMax : max;
                else
                    max = preMax.Sum(s => s.Value) > currMax.Sum(s => s.Value) ? preMax : currMax;

                DataTable.Rows[r][c] = max;
            }
        }

        List<DynamicProgrammingItem> GetMaxItems() => (List<DynamicProgrammingItem>)DataTable.Rows[DataTable.Rows.Count - 1][DataTable.Columns.Count - 1];

        public override string ToString()
        {
            List<DynamicProgrammingItem> items = GetMaxItems();
            string s = "";
            items.ForEach(f => s += $"[N:{f.Name} W:{f.Weight} V:{f.Value}] + ");
            return $"{s[0..^3]} Result => W:{items.Sum(s => s.Weight)} V:{items.Sum(s => s.Value)}";
        }
    }
}
