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
        public Player() : base("dog", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadPlayerAnimations();
            Position = Game.ScreenCenter;

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.PLAYER;
            RigidBody.AddCollisionType(RigidBodyType.TILE);

            animation = GfxManager.GetAnimation("IdleD");
            animation.Start();

            IsActive = true;
        }

        public void Input()
        {
            if (IsActive)
            {
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
