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
        private int xOff, yOff;

        public int Weight { get; private set; }

        public TmxObject(string objName, int offsetX, int offsetY, int w, int h, bool solid) : base("tileset", DrawLayer.MIDDLEGROUND, w, h)
        {
            xOff = offsetX;
            yOff = offsetY;

            if (solid)
            {
                RigidBody = new RigidBody(this);
                RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
                RigidBody.Collider.Offset = new Vector2(0.23f, 0.23f);
                RigidBody.Type = RigidBodyType.TILE;

                Weight = int.MaxValue;
            }
            else
            {
                Weight = 1;
            }

            IsActive = true;
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, 16, 16);
            }
        }
    }
}
