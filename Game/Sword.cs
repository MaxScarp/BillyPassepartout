using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class Sword : GameObject
    {
        public delegate void SwordCollectedEvent(object sender);
        public event SwordCollectedEvent OnSwordCollected;

        public AudioSource AudioSource { get; private set; }
        public float Speed { get; private set; }
        public float RotationSpeed { get; private set; }

        public Sword() : base("sword", DrawLayer.FOREGROUND)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this, Game.PixelsToUnits(5.0f));
            RigidBody.Collider.Offset = new Vector2(0.33f, 0.33f);
            RigidBody.Type = RigidBodyType.SWORD;

            if(!PersistentData.IsSwordCollected)
            {
                RigidBody.AddCollisionType(RigidBodyType.PLAYER);
            }
            else
            {
                RigidBody.AddCollisionType(RigidBodyType.ENEMY);
            }

            Speed = 5.5f;
            RotationSpeed = 4.5f;

            AudioSource = new AudioSource();
        }

        public override void Update()
        {
            if(IsActive)
            {
                if(IsOutOfScreen)
                {
                    IsActive = false;
                    AudioSource.Stop();
                }
                
                if(PersistentData.IsSwordCollected)
                {
                    Sprite.Rotation -= Game.DeltaTime * RotationSpeed;
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(!PersistentData.IsSwordCollected)
            {
                SwordCollected();
            }
            else
            {
                AudioSource.Stop();
                IsActive = false;
            }
        }

        private void SwordCollected()
        {
            OnSwordCollected?.Invoke(this);
        }
    }
}