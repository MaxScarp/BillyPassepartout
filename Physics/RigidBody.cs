using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    enum RigidBodyType { Player = 1, Tile = 2 }

    class RigidBody
    {
        public Vector2 Velocity;

        public GameObject GameObject;
        public bool IsGravityAffected;
        public bool IsCollisionAffected = true;
        public float Friction;

        public Collider Collider;

        public RigidBodyType Type;

        protected uint collisionMask;

        public bool IsActive { get { return GameObject.IsActive; } }

        public Vector2 Position { get { return GameObject.Position; } }

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
            PhysicsManager.AddItem(this);
        }

        public void Update()
        {
            if (IsGravityAffected)
            {
                Velocity.Y += PhysicsManager.G * Game.DeltaTime;
            }

            // Friction
            ApplyFriction();

            GameObject.Position += Velocity * Game.DeltaTime;
        }

        protected void ApplyFriction()
        {
            if(Friction > 0 && Velocity != Vector2.Zero)
            {
                float fAmount = Friction * Game.DeltaTime;
                float newVelocityLength = Velocity.Length - fAmount;

                if(newVelocityLength < 0)
                {
                    Velocity = Vector2.Zero;
                    return;
                }

                Velocity = Velocity.Normalized() * newVelocityLength;
            }
        }

        public bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;
        }

        public void AddCollisionType(uint type)
        {
            collisionMask |= type;
        }

        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionMask) != 0;
        }

        public void Destroy()
        {
            GameObject = null;
            Collider = null;

            PhysicsManager.RemoveItem(this);
        }
    }
}
