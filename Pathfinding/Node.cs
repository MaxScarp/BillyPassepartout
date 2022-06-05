using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    class Node : IDrawable, IUpdatable
    {
        private Sprite sprite;
        private bool isDrawable;

        public int X { get; }
        public int Y { get; }
        public int Cost { get; private set; }

        public List<Node> Neighbours { get; }

        public DrawLayer Layer { get; private set; }

        public Node(int x, int y, int cost)
        {
            Layer = DrawLayer.GUI;

            X = x;
            Y = y;
            Cost = cost;
            Neighbours = new List<Node>();

            sprite = new Sprite(1, 1);
            sprite.position = new Vector2(X, Y);

            isDrawable = false;

            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);
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
        }

        public void Update()
        {
            if ((int)Game.MousePos.X == sprite.position.X && (int)Game.MousePos.Y == sprite.position.Y)
            {
                isDrawable = true;
            }
            else
            {
                isDrawable = false;
            } 
        }

        public void Draw()
        {
            if (isDrawable)
            {
                switch (Cost)
                {
                    case 1:
                        sprite.DrawColor(new Vector4(0.0f, 1.0f, 0.0f, 0.4f));
                        break;
                    case 2:
                        sprite.DrawColor(new Vector4(0.0f, 0.0f, 1.0f, 0.4f));
                        break;
                    case 3:
                        sprite.DrawColor(new Vector4(0.729f, 0.698f, 0.090f, 0.4f));
                        break;
                    case 4:
                        sprite.DrawColor(new Vector4(0.749f, 0.094f, 0.223f, 0.4f));
                        break;
                    default:
                        sprite.DrawColor(new Vector4(1.0f, 0.0f, 0.0f, 0.4f));
                        break;
                }
            } 
        }
    }
}
