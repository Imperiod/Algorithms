using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    /// <summary>
    /// Node as node of tree and node as top node
    /// </summary>
    public class Node
    {
        List<Node> Childrens { get; }

        public int Index { get; }

        public double Weight { get; }

        public Node(int Index, double Weight = 1, List<Node> Childrens = default)
        {
            this.Index = Index;
            this.Weight = Weight;
            this.Childrens = Childrens;
        }

        /// <summary>
        /// Use only after rebuild
        /// </summary>
        /// <param name="node">Top node</param>
        /// <returns>string like [index]:weight->...</returns>
        public string ToString(Node node)
        {
            if (node.Childrens?.Count == 1)
            {
                return $"[{node.Index}]:{node.Weight} -> " + ToString(node.Childrens[0]);
            }
            else if (node.Childrens?.Count > 1)
            {
                return null;
            }
            else
            {
                return $"[{node.Index}]:{node.Weight}";
            }
        }

        /// <summary>
        /// Gets the short path from the top node to the node that was specified as LastIndex
        /// </summary>
        /// <param name="LastIndex">Index of last node in tree</param>
        /// <param name="node">Top node</param>
        /// <returns></returns>
        public List<(int, double)> GetShortWayToNode(int LastIndex, Node node)
        {
            List<List<(int, double)>> result = new List<List<(int, double)>>();

            if (node.Childrens is null || node.Index == LastIndex)
            {
                return new List<(int, double)>() { (node.Index, node.Weight) };
            }
            else
            {
                foreach (var item in node.Childrens)
                {
                    List<(int, double)> list = new List<(int, double)>();
                    list.Add((node.Index, node.Weight));
                    var v = GetShortWayToNode(LastIndex, item);
                    v.ForEach(f => list.Add(f));
                    result.Add(list);
                }

                return result.First(f => 
                                        f.Sum(s => s.Item2) == result.Min(m => m.Sum(ms => ms.Item2)));
            }
        }

        /// <summary>
        /// Build new tree
        /// </summary>
        /// <param name="list">Get from GetShortWayToNode</param>
        /// <param name="index">Don't use it</param>
        /// <returns>New node with childrens</returns>
        public Node Rebuild(List<(int, double)> list, int index = 0)
        {
            if (index == list.Count - 1)
            {
                return new Node(list.Last().Item1, list.Last().Item2);
            }
            else
            {
                return new Node(list[index].Item1, list[index].Item2, new List<Node>() { Rebuild(list, index + 1) });
            }
        }
    }
}
