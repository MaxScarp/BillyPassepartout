using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    class GameObject : IUpdatable, IDrawable
    {
        protected Sprite sprite;
        protected Texture texture;
        public RigidBody RigidBody;

        public bool IsActive;

        public virtual Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }
        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }
        public float HalfWidth { get { return sprite.Width * 0.5f; } }
        public float HalfHeight { get { return sprite.Height * 0.5f; } }
        public float Width { get { return sprite.Width; } }
        public float Height { get { return sprite.Height; } }

        public Vector2 Forward
        { 
            get
            {
                return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            }
            set
            {
                sprite.Rotation = (float)Math.Atan2(value.Y, value.X);
            }
        }

        public DrawLayer Layer { get; protected set; }

        public GameObject(string textureName, DrawLayer layer = DrawLayer.PLAYGROUND, float w = 0, float h = 0)
        {
            texture = GfxManager.GetTexture(textureName);

            float spriteW = w != 0 ? w : Game.PixelsToUnits(texture.Width);
            float spriteH = h != 0 ? h : Game.PixelsToUnits(texture.Height);

            sprite = new Sprite(spriteW, spriteH);

            sprite.pivot = new Vector2(Game.PixelsToUnits(HalfWidth), Game.PixelsToUnits(HalfHeight));

            Layer = layer;

            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);
        }

        public virtual void Update() { }

        public virtual void OnCollide(Collision collisionInfo) { }

        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture);
            }
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);
        }
    }
}
