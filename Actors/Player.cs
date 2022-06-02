using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;

namespace BillyPassepartout
{
    class Player : Actor
    {
        private bool isMouseLeftClicked;

        public Player() : base("dog", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadPlayerAnimations();
            Position = Game.ScreenCenter;
            
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Collider.Offset = new Vector2(0.23f, 0.23f);
            RigidBody.Type = RigidBodyType.PLAYER;
            RigidBody.AddCollisionType(RigidBodyType.TILE);

            animation = GfxManager.GetAnimation("IdleD");
            animation.Start();

            agent = new Agent(this);

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
                        Vector2 mousePos = new Vector2(Game.Window.MouseX, Game.Window.MouseY);
                        List<Node> path = ((PlayScene)SceneManager.CurrentScene).Map.PathfindingMap.GetPath(agent.X, agent.Y, (int)mousePos.X, (int)mousePos.Y);
                        agent.SetPath(path);
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
                base.Update();
            } 
        }
        public override void Draw()
        {
            if(IsActive)
            {
                base.Draw();
            }
        }
    }
}
