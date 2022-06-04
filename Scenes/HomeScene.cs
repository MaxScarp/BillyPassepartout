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
    class HomeScene : Scene
    {
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
            Map = null;
            Player = null;
            Key = null;
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
            Player.Position = Game.ScreenCenter;
        }

        private void LoadObjects()
        {
            Key = new Key();
            Key.IsActive = true;
            Key.Position = new Vector2(10, 2);
            Key.OnKeyCollected += KeyCollected;

            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name == "Door")
                {
                    obj.OnDoorReached += DoorReached;
                    break;
                }
            }
        }

        private void KeyCollected(object sender)
        {
            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name.Contains("Door"))
                {
                    Key.OnKeyCollected -= KeyCollected;
                    Map.PathfindingMap.ToggleNode(obj.X, obj.Y, 2);
                    Key.IsActive = false;
                    break;
                }
            }
        }

        private void DoorReached(object sender)
        {
            ((TmxObject)sender).OnDoorReached -= DoorReached;
            IsPlaying = false;
        }
    }
}
