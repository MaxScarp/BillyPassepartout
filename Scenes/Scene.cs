using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    abstract class Scene
    {
        public bool IsKeyCollected;
        public bool IsSwordCollected;
        public Vector2 PlayerStartingPos;

        public TmxMap Map { get; protected set; }
        public Player Player { get; protected set; }
        public List<Enemy> Enemies { get; protected set; }
        public Key Key { get; protected set; }
        public Sword Sword { get; protected set; }
        public bool IsPlaying { get; protected set; }

        public Scene()
        {
            Enemies = new List<Enemy>();

            SceneManager.AddScene(this);
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual void OnExit()
        {
            IsPlaying = false;

            CameraManager.ClearAll();
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

        public void Quit()
        {
            if (Game.Window.GetKey(KeyCode.Esc))
            {
                Game.Window.Exit();
            }
        }
    }
}
