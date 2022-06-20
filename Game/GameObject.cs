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
        protected Texture texture;
        public RigidBody RigidBody;

        public bool IsActive;

        public Sprite Sprite { get; protected set; }
        public virtual Vector2 Position { get { return Sprite.position; } set { Sprite.position = value; } }
        public int X { get { return (int)Sprite.position.X; } set { Sprite.position.X = value; } }
        public int Y { get { return (int)Sprite.position.Y; } set { Sprite.position.Y = value; } }
        public float HalfWidth { get { return Sprite.Width * 0.5f; } }
        public float HalfHeight { get { return Sprite.Height * 0.5f; } }
        public float Width { get { return Sprite.Width; } }
        public float Height { get { return Sprite.Height; } }
        public bool IsOutOfScreen { get { return CalculateOutOfScreen(); } }

        public Vector2 Forward
        { 
            get
            {
                return new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation));
            }
            set
            {
                Sprite.Rotation = (float)Math.Atan2(value.Y, value.X);
            }
        }

        public int Direction { get { return Math.Sign(RigidBody.Velocity.X); } }

        public DrawLayer Layer { get; protected set; }

        public GameObject(string textureName, DrawLayer layer = DrawLayer.PLAYGROUND, float w = 0, float h = 0)
        {
            texture = GfxManager.GetTexture(textureName);

            float spriteW = w != 0 ? w : Game.PixelsToUnits(texture.Width);
            float spriteH = h != 0 ? h : Game.PixelsToUnits(texture.Height);

            Sprite = new Sprite(spriteW, spriteH);

            //Sprite.pivot = new Vector2(Game.PixelsToUnits(HalfWidth), Game.PixelsToUnits(HalfHeight));

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
                Sprite.DrawTexture(texture);
            }
        }

        public virtual void Destroy()
        {
            Sprite = null;
            texture = null;

            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);
        }

        private bool CalculateOutOfScreen()
        {
            return X < 0 || X >= Game.Window.OrthoWidth || Y < 0 || Y >= Game.Window.OrthoHeight;
        }
    }
}
