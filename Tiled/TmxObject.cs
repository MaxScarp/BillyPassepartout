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
        protected int xOff, yOff;

        public int Weight { get; private set; }
        public string Name { get; private set; }

        public TmxObject(string objName, int offsetX, int offsetY, int w, int h, bool solid) : base("tileset", DrawLayer.MIDDLEGROUND, w, h)
        {
            Name = objName;
            xOff = offsetX;
            yOff = offsetY;

            if (solid)
            {
                Weight = int.MaxValue;
            }
            else
            {
                Weight = 1;
            }

            IsActive = true;
        }

        public override void Update()
        {
            if(IsActive)
            {
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                Sprite.DrawTexture(texture, xOff, yOff, 16, 16);
            }
        }
    }
}
