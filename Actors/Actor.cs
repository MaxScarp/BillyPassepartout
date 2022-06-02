using Aiv.Audio;
using OpenTK;

namespace BillyPassepartout
{
    abstract class Actor : GameObject
    {
        protected Animation animation;

        public Actor(string textureName, float w = 0, float h = 0) : base(textureName, w:w, h:h)
        {
            RigidBody = new RigidBody(this);
        }

        public override void Update()
        {
            animation.Update();
        }

        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        protected virtual void Attack() { }
    }
}
