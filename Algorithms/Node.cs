﻿using System;
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
        public List<Node> Childrens { get; set; }

        public int Index { get; }

        public double Weight { get; }

        /// <summary>
        /// Classic realisation
        /// </summary>
        /// <param name="Index">Index node in tree</param>
        /// <param name="Weight">Weight of node from other node, as from A to B = 10</param>
        /// <param name="Childrens">Childrens this node, whichs have own childrens</param>
        public Node(int Index, double Weight = 1, List<Node> Childrens = null)
        {
            this.Index = Index;
            this.Weight = Weight;
            this.Childrens = Childrens is null ? new List<Node>() : Childrens;
        }

        /// <summary>
        /// Build with service class NodePoint
        /// </summary>
        /// <param name="nodePoints">list of NodePoint</param>
        public Node(List<NodePoint> nodePoints)
        {
            var x = Build(nodePoints, nodePoints.First());
            Index = x.First().Index;
            Weight = x.First().Weight;
            Childrens = x.First().Childrens;
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
                if (node.Index == 0)
                {

                }
                else
                {

                }
                return $"[{node.Index}]:{node.Weight} -> " + ToString(node.Childrens[0]) + (node.Index == 0 ? $"\nAll path will be equal {GetSumofPath(node)} weight" : "");
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

        double GetSumofPath(Node node)
        {
            double sum = default;
            if (node.Childrens.Count != 0)
                sum += GetSumofPath(node.Childrens.First()) + node.Weight;
            else
                sum += node.Weight;
            
            return sum;
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

            if (node.Index == LastIndex)
            {
                return new List<(int, double)>() { (node.Index, node.Weight) };
            }
            else if(node.Childrens is null || node.Childrens?.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var item in node.Childrens)
                {
                    List<(int, double)> list = new List<(int, double)>();
                    list.Add((node.Index, node.Weight));
                    var v = GetShortWayToNode(LastIndex, item);
                    if (v != null)
                    {
                        v.ForEach(f => list.Add(f));
                        result.Add(list);
                    }
                }

                //Protector
                if (result.Count == 0)
                {
                    return null;
                }
                else
                {
                    return result.First(f =>
                                        f.Sum(s => s.Item2) == result.Min(m => m.Sum(ms => ms.Item2)));
                }
                
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

        List<Node> Build(List<NodePoint> list, NodePoint point, int index = 0)
        {
            foreach (var p in list)
            {
                List<Node> nodes = new List<Node>();
                foreach (var item in point.Points)
                {
                    if (point.Points.First() == (0, 0))
                    {
                        (int, double) x = item;
                        point.Points.Remove(item);
                        return new List<Node>() { new Node(x.Item1, x.Item2, Build(list, point)) };
                    }
                    else if (list.Count == item.Item1)
                    {
                        nodes.Add(new Node(item.Item1, item.Item2));
                        return nodes;
                    }
                    else if (item.Item1 < index)
                    {
                        continue;
                    }
                    else
                    {
                        nodes.Add(new Node(item.Item1, item.Item2, Build(list, list[item.Item1], item.Item1)));
                    }
                    
                    //return new List<Node>() { new Node(item.Item1, item.Item2, nodes) };
                }
                return nodes;
            }
            return null;
        }
    }
}
