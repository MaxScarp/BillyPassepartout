using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class Player : Actor
    {
        private bool isMouseLeftClicked;

        public Player() : base("dog", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadPlayerAnimations();

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
            RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
            RigidBody.Type = RigidBodyType.PLAYER;

            Animation = GfxManager.GetAnimation("idleD");
            Animation.Start();

            Agent = new Agent(this);

            isMouseLeftClicked = false;

            IsActive = true;
        }

        public void Input()
        {
            if (IsActive)
            {
                if (Game.Window.MouseLeft)
                {
                    if (!isMouseLeftClicked)
                    {
                        isMouseLeftClicked = true;

                        List<Node> path = (SceneManager.CurrentScene).Map.PathfindingMap.GetPath(Agent.X, Agent.Y, (int)Game.MousePos.X, (int)Game.MousePos.Y);
                        Agent.SetPath(path);
                    }
                }
                else if (isMouseLeftClicked)
                {
                    isMouseLeftClicked = false;
                }
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                Node lastNode = Agent.GetLastNode();
                if(lastNode != null)
                {
                    if(lastNode.X > (int)Position.X)
                    {
                        Sprite.FlipX = false;
                        Animation = GfxManager.GetAnimation("walkR");
                    }
                    else
                    {
                        Sprite.FlipX = true;
                        Animation = GfxManager.GetAnimation("walkR");
                    }
                }
                else
                {
                    if(RigidBody.Velocity == Vector2.Zero)
                    {
                        Animation = GfxManager.GetAnimation("idleD");
                    }
                }

                Animation.Start();
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
            }
        }
    }
}
