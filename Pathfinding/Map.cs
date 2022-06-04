using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    class Map
    {
        private Dictionary<Node, Node> cameFrom;
        private Dictionary<Node, int> costSoFar;
        private PriorityQueue frontier;

        private int width;
        private int height;
        private int[] cells;

        public Node[] Nodes { get; }

        public Map(int width, int height, TmxObjectLayer objectLayer)
        {
            this.width = width;
            this.height = height;
            cells = objectLayer.Cells;

            Nodes = new Node[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                if(cells[i] > 0) 
                {
                    Nodes[i] = new Node(x, y, cells[i]);
                }
                else
                {
                    Nodes[i] = new Node(x, y, 1);
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    if (Nodes[index].Cost == int.MaxValue)
                    {
                        continue;
                    }

                    AddNeighbours(Nodes[index], x, y);
                }
            }
        }

        void AddNeighbours(Node node, int x, int y)
        {
            CheckNeighbours(node, x, y - 1);
            CheckNeighbours(node, x, y + 1);
            CheckNeighbours(node, x + 1, y);
            CheckNeighbours(node, x - 1, y);
        }

        public void CheckNeighbours(Node currentNode, int cellX, int cellY)
        {
            if(cellX < 0 || cellX >= width)
            {
                return;
            }

            if (cellY < 0 || cellY >= height)
            {
                return;
            }

            int index = cellY * width + cellX;

            Node neighbour = Nodes[index];

            if(neighbour.Cost != int.MaxValue)
            {
                currentNode.AddNeighbour(neighbour);
            }
        }

        void AddNode(int x, int y, int cost)
        {
            int index = y * width + x;
            Nodes[index].SetCost(cost);
            AddNeighbours(Nodes[index], x, y);

            foreach(Node adj in Nodes[index].Neighbours)
            {
                adj.AddNeighbour(Nodes[index]);
            }

            cells[index] = cost;
        }

        void RemoveNode(int x, int y)
        {
            int index = y * width + x;
            Node node = GetNode(x, y);

            foreach(Node adj in node.Neighbours)
            {
                adj.RemoveNeighbour(node);
            }

            Nodes[index].SetCost(int.MaxValue);
            cells[index] = int.MaxValue;
        }

        public Node GetNode(int x, int y)
        {
            if((x >= width || x < 0) || (y >= height || y < 0)) { return null; }

            return Nodes[y * width + x];
        }

        public Node GetRandomNode()
        {
            Node randomNode = null;

            do
            {
                randomNode = Nodes[RandomGenerator.GetRandomInt(0, Nodes.Count())];
            } while (randomNode.Cost == int.MaxValue);

            return randomNode;
        } //TODO

        public void ToggleNode(int x, int y, int cost = 1)
        {
            Node node = GetNode(x, y);

            if(node.Cost == int.MaxValue)
            {
                AddNode(x, y, cost);
            }
            else
            {
                RemoveNode(x, y);
            }
        }

        public List<Node> GetPath(int startX, int startY, int endX, int endY)
        {
            List<Node> path = new List<Node>();

            Node start = GetNode(startX, startY);
            Node end = GetNode(endX, endY);

            if(start.Cost == int.MaxValue || end.Cost == int.MaxValue)
            {
                return path;
            }

            AStar(start, end);

            if(!cameFrom.ContainsKey(end))
            {
                return path;
            }

            Node currNode = end;

            while(currNode != cameFrom[currNode])
            {
                path.Add(currNode);
                currNode = cameFrom[currNode];
            }

            path.Reverse();

            return path;
        }

        public void AStar(Node start, Node end)
        {
            cameFrom = new Dictionary<Node, Node>();
            costSoFar = new Dictionary<Node, int>();
            frontier = new PriorityQueue();

            cameFrom[start] = start;
            costSoFar[start] = 0;
            frontier.Enqueue(start, Heuristic(start, end));

            while (!frontier.IsEmpty)
            {
                Node currNode = frontier.Dequeue();

                if (currNode == end)
                {
                    return;
                }

                foreach (Node nextNode in currNode.Neighbours)
                {
                    int newCost = costSoFar[currNode] + nextNode.Cost;

                    if (!costSoFar.ContainsKey(nextNode) || costSoFar[nextNode] > newCost)
                    {
                        cameFrom[nextNode] = currNode;
                        costSoFar[nextNode] = newCost;
                        int priority = newCost + Heuristic(nextNode, end);
                        frontier.Enqueue(nextNode, priority);
                    }
                }
            }
        }

        private int Heuristic(Node start, Node end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }
    }
}
