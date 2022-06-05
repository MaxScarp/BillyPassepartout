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
        public List<Node> Path { get; private set; }
        public Node Target { get; private set; }

        private Node current;

        private Actor owner;

        public Agent(Actor owner)
        {
            this.owner = owner;
            Target = null;

            UpdateManager.AddItem(this);
        }

        public virtual void SetPath(List<Node> newPath)
        {
            Path = newPath;

            if(Target == null && Path.Count > 0)
            {
                Target = Path[0];
                Path.RemoveAt(0);
            }
            else if(Path.Count > 0)
            {
                int dist = Math.Abs(Path[0].X - Target.X) + Math.Abs(Path[0].Y - Target.Y);

                if(dist > 1)
                {
                    Path.Insert(0, current);
                }
            }
        }

        public void ResetPath()
        {
            if (Path != null)
            {
                Path.Clear();
            }

            Target = null;
        }

        public Node GetLastNode()
        {
            if(Path != null && Path.Count > 0)
            {
                return Path.Last();
            }

            return null;
        }

        public void Update()
        {
            if(Target != null)
            {
                Vector2 destination = new Vector2(Target.X, Target.Y);
                Vector2 direction = (destination - owner.Position);
                float distance = direction.Length;

                if (distance < 0.05f)
                {
                    current = Target;
                    owner.Position = destination;
                    
                    if(Path.Count == 0)
                    {
                        Target = null;
                    }
                    else
                    {
                        Target = Path[0];
                        Path.RemoveAt(0);
                    }
                }
                else
                {
                    owner.RigidBody.Velocity = direction.Normalized() * owner.Speed * Game.DeltaTime;
                }
            }
            else
            {
                owner.RigidBody.Velocity = Vector2.Zero;
            }
        }
    }
}
