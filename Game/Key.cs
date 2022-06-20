using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class Key : GameObject
    {
        public delegate void KeyCollectedEvent(object sender);
        public event KeyCollectedEvent OnKeyCollected;

        public AudioSource AudioSource { get; private set; }

        public Key() : base("key", DrawLayer.FOREGROUND)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, Game.PixelsToUnits(12), Game.PixelsToUnits(12));
            RigidBody.Collider.Offset = new Vector2(0.3f, 0.3f);
            RigidBody.Type = RigidBodyType.KEY;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            AudioSource = new AudioSource();
        }

        public override void OnCollide(Collision collisionInfo)
        {
            KeyCollected();
        }

        private void KeyCollected()
        {
            OnKeyCollected?.Invoke(this);
        }
    }
}
