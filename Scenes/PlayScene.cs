using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    class PlayScene : Scene
    {
        public TmxMap Map { get; private set; }
        public Player Player { get; private set; }
        public Key Key { get; private set; }

        public override void Start()
        {
            LoadAssets();
            LoadAudio();
            LoadMap();
            LoadPlayer();
            LoadObjects();
            
            base.Start();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Input()
        {
            Quit();
            Player.Input();
        }

        public override void Update()
        {
            UpdateManager.Update();
            PhysicsManager.Update();

            PhysicsManager.CheckCollisions();
        }

        public override void Draw()
        {
            DrawManager.Draw();
        }

        private void LoadAssets()
        {
            //Map
            GfxManager.AddTexture("tileset", "Assets/Tileset.png");

            //Player
            GfxManager.AddTexture("dog", "Assets/Hero/Dog.png");

            //Objects
            GfxManager.AddTexture("key", "Assets/Objects/Key.png");
        }

        private void LoadAudio() { }

        private void LoadMap()
        {
            Map = new TmxMap("Tiled/XML/HomeMap.xml");
        }

        private void LoadPlayer()
        {
            Player = new Player();
        }

        private void LoadObjects()
        {
            Key = new Key();
            Key.OnKeyCollected += KeyCollected;
        }

        private void KeyCollected(object sender)
        {
            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name == "Door")
                {
                    Map.PathfindingMap.ToggleNode(obj.X, obj.Y, 2);
                    Key.IsActive = false;
                    Key.OnKeyCollected -= KeyCollected;
                    break;
                }
            }
        }
    }
}
