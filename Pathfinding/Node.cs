﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    class Node
    {
        public int X { get; }
        public int Y { get; }
        public int Cost { get; private set; }

        public List<Node> Neighbours { get; }

        public Node(int x, int y, int cost)
        {
            X = x;
            Y = y;
            Cost = cost;
            Neighbours = new List<Node>();
        }

        public void AddNeighbour(Node node)
        {
            Neighbours.Add(node);
        }

        public void RemoveNeighbour(Node node)
        {
            Neighbours.Remove(node);
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        } //TODO
    }
}
