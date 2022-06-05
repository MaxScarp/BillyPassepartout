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
        public delegate void OnDoorReachedEvent(object sender);
        public event OnDoorReachedEvent OnDoorReached;

        private int xOff, yOff;

        public int Weight { get; private set; }
        public string Name { get; private set; }

        public TmxObject(string objName, int offsetX, int offsetY, int w, int h, bool solid = false, bool open = false, bool close = false) : base("tileset", DrawLayer.MIDDLEGROUND, w, h)
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
            DoorReached();
        }

        private void DoorReached()
        {
            OnDoorReached?.Invoke(this);
        }
    }
}
