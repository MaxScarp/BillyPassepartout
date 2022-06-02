using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    class Agent : IUpdatable
    {
        public int X { get { return Convert.ToInt32(owner.Position.X); } }
        public int Y { get { return Convert.ToInt32(owner.Position.Y); } }

        private Node current;
        private Node target;
        private List<Node> path;

        public DrawLayer Layer { get; private set; }

        private Actor owner;

        public Agent(Actor owner)
        {
            this.owner = owner;
            target = null;

            Layer = DrawLayer.FOREGROUND;

            UpdateManager.AddItem(this);
        }

        public virtual void SetPath(List<Node> newPath)
        {
            path = newPath;

            if(target == null && path.Count > 0)
            {
                target = path[0];
                path.RemoveAt(0);
            }
            else if(path.Count > 0)
            {
                int dist = Math.Abs(path[0].X - target.X) + Math.Abs(path[0].Y - target.Y);

                if(dist > 1)
                {
                    path.Insert(0, current);
                }
            }
        }

        public void ResetPath()
        {
            if(path != null)
            {
                path.Clear();
            }

            target = null;
        } //TODO

        public Node GetLastNode()
        {
            if(path.Count > 0)
            {
                return path.Last();
            }

            return null;
        } //TODO

        public void Update()
        {
            if(target != null)
            {
                Vector2 destination = new Vector2(target.X, target.Y);
                Vector2 direction = (destination - owner.Position);
                float distance = direction.Length;

                if (distance < 0.01f)
                {
                    current = target;
                    owner.Position = destination;
                    
                    if(path.Count == 0)
                    {
                        target = null;
                        owner.RigidBody.Velocity = Vector2.Zero;
                    }
                    else
                    {
                        target = path[0];
                        path.RemoveAt(0);
                    }
                }
                else
                {
                    owner.RigidBody.Velocity = direction.Normalized() * owner.Speed * Game.DeltaTime;
                }
            }
        }
    }
}
