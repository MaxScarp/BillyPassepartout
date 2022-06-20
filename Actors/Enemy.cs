using Aiv.Audio;
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

        public AudioSource AudioSource { get; private set; }

        public Enemy() : base("ghost", Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            AnimationStorage.LoadEnemyAnimations();

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
            RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
            RigidBody.Type = RigidBodyType.ENEMY;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER | RigidBodyType.SWORD);

            Animation = GfxManager.GetAnimation("ghostIdleD");
            Animation.Start();

            Speed = 1.5f;

            visionRay = 4;

            isDead = false;

            AudioSource = new AudioSource();
            AudioManager.AddClip("ghostDie", "Assets/Sounds/GhostDie.ogg");
            AudioManager.AddClip("follow", "Assets/Sounds/GhostFollowing.ogg");

            IsActive = true;
        }

        public override void Update()
        {
            if (!isDead && IsActive)
            {
                Vector2 distFromPlayer = SceneManager.CurrentScene.Player.Position - Position;
                if (distFromPlayer.LengthSquared <= visionRay * visionRay)
                {
                    if(!AudioSource.IsPlaying)
                    {
                        AudioSource.Play(AudioManager.GetClip("follow"));
                    }
                    RigidBody.Velocity = distFromPlayer.Normalized() * Speed * Game.DeltaTime;
                }
                else
                {
                    if(AudioSource.IsPlaying)
                    {
                        AudioSource.Stop();
                    }
                    RigidBody.Velocity = Vector2.Zero;
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (!isDead)
            {
                isDead = true;
                if(collisionInfo.Collider.RigidBody.GameObject is Player)
                {
                    SceneManager.CurrentScene.Player.GeneralSource.Play(AudioManager.GetClip("hurt"));
                    SceneManager.CurrentScene.Player.Lives--;
                    Console.WriteLine(SceneManager.CurrentScene.Player.Lives);
                }
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
            Position -= RigidBody.Velocity * Game.DeltaTime * Speed * 2.5f;
            AudioSource.Play(AudioManager.GetClip("ghostDie"));
            Animation = GfxManager.GetAnimation("ghostDie");
            Animation.Start();
            SceneManager.CurrentScene.Enemies.Remove(this);
        }
    }
}
