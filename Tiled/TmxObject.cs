using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class TmxObject : GameObject
    {
        public delegate void OnSomethingReachedEvent(object sender);
        public event OnSomethingReachedEvent OnDoorReached;
        public event OnSomethingReachedEvent OnButtonReached;

        private int xOff, yOff;

        public int Weight { get; private set; }
        public string Name { get; private set; }

        public TmxObject(string objName, int offsetX, int offsetY, int w, int h, bool solid = false, bool open = false, bool close = false, bool pressed = false, bool active = false) : base("tileset", DrawLayer.MIDDLEGROUND, w, h)
        {
            Name = objName;
            xOff = offsetX;
            yOff = offsetY;

            if(solid)
            {
                Weight = int.MaxValue;
            }

            if(objName.Contains("Door"))
            {
                if (open || SceneManager.CurrentScene.IsKeyCollected)
                {
                    Weight = 2;
                }

                RigidBody = new RigidBody(this);
                RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
                RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
                RigidBody.Type = RigidBodyType.DOOR;
                RigidBody.AddCollisionType(RigidBodyType.PLAYER);
            }

            if(objName.Contains("Button"))
            {
                if(!pressed)
                {
                    Weight =  3;

                    RigidBody = new RigidBody(this);
                    RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
                    RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
                    RigidBody.Type = RigidBodyType.BUTTON;
                    RigidBody.AddCollisionType(RigidBodyType.PLAYER);
                }
                else
                {
                    Weight = 1;
                }
                
            }

            if (objName.Contains("Trap"))
            {
                Weight = active ? 4 : 1;

                if(active)
                {
                    Weight = 4;

                    RigidBody = new RigidBody(this);
                    RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
                    RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
                    RigidBody.Type = RigidBodyType.TRAP;
                    RigidBody.AddCollisionType(RigidBodyType.PLAYER);
                }
                else
                {
                    Weight = 1;
                }
            }

            IsActive = true;
        }

        public override void Draw()
        {
            if (IsActive)
            {
                Sprite.DrawTexture(texture, xOff, yOff, 16, 16);
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(RigidBody.Type == RigidBodyType.DOOR)
            {
                DoorReached();
            }
            else if(RigidBody.Type == RigidBodyType.BUTTON)
            {
                ButtonReached();
            }
        }

        private void DoorReached()
        {
            if(OnDoorReached != null)
            {
                OnDoorReached(this);
            }
        }

        private void ButtonReached()
        {
            if(OnButtonReached != null)
            {
                OnButtonReached(this);
            }
        }
    }
}
