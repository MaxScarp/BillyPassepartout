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
        private int visionRay;
        private bool isDead;

        public Enemy() : base("ghost", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadEnemyAnimations();

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
            RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
            RigidBody.Type = RigidBodyType.ENEMY;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            Animation = GfxManager.GetAnimation("ghostIdleD");
            Animation.Start();

            Speed = 1.5f;

            visionRay = 4;

            isDead = false;

            IsActive = true;
        }

        public override void Update()
        {
            if (!isDead && IsActive)
            {
                Vector2 distFromPlayer = SceneManager.CurrentScene.Player.Position - Position;
                if (distFromPlayer.LengthSquared <= visionRay * visionRay)
                {
                    RigidBody.Velocity = distFromPlayer.Normalized() * Speed * Game.DeltaTime;
                }
                else
                {
                    RigidBody.Velocity = Vector2.Zero;
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            //TODO ADD DAMAGE
            if (!isDead)
            {
                isDead = true;
                Die();
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
            }
        }

        private void Die()
        {
            RigidBody.Velocity = Vector2.Zero;
            Animation = GfxManager.GetAnimation("ghostDie");
            Animation.Start();
            SceneManager.CurrentScene.Enemies.Remove(this);
        }
    }
}
