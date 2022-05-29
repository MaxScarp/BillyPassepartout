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

        public TmxObject(string objName, int offsetX, int offsetY, int w, int h, bool solid) : base("tileset", DrawLayer.Middleground, w, h)
        {
            xOff = offsetX;
            yOff = offsetY;

            if (solid)
            {
                RigidBody = new RigidBody(this);
                RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
                RigidBody.Type = RigidBodyType.Tile;
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
