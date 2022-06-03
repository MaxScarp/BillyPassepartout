using Aiv.Audio;
using OpenTK;

namespace BillyPassepartout
{
    abstract class Actor : GameObject
    {
        public Animation Animation;

        public Agent Agent { get; protected set; }
        public float Speed { get; protected set; }

        public Actor(string textureName, float w = 0, float h = 0) : base(textureName, w:w, h:h)
        {
            RigidBody = new RigidBody(this);
            Speed = 5.0f;
        }

        public override void Draw()
        {
            if(IsActive)
            {
                Sprite.DrawTexture(texture, Animation.XOffset, Animation.YOffset, Animation.FrameWidth, Animation.FrameHeight);
            }
        }

        protected virtual void Attack() { }
    }
}
