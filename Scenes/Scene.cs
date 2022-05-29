using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }

        public Scene()
        {
            SceneManager.AddScene(this);
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual void OnExit()
        {
            AudioManager.ClearAll();
            DebugManager.ClearAll();
            DrawManager.ClearAll();
            FontManager.ClearAll();
            GfxManager.ClearAll();
            PhysicsManager.ClearAll();
            UpdateManager.ClearAll();
        }

        public abstract void Input();
        public abstract void Update();
        public abstract void Draw();

        protected void Quit()
        {
            if (Game.Window.GetKey(KeyCode.Esc))
            {
                Game.Window.Exit();
            }
        }
    }
}
