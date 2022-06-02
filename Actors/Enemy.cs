using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace BillyPassepartout
{
    class Enemy : Actor
    {
        public int Damage { get; }

        public Enemy() : base ("default") { }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
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
