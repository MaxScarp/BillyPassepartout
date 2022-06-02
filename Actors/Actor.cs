using Aiv.Audio;
using OpenTK;

namespace BillyPassepartout
{
    abstract class Actor : GameObject
    {
        protected Animation animation;
        protected Agent agent;

        public float Speed { get; protected set; }

        public Actor(string textureName, float w = 0, float h = 0) : base(textureName, w:w, h:h)
        {
            RigidBody = new RigidBody(this);
            Speed = 5.5f;
        }

        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }

        protected virtual void Attack() { }
    }
}
