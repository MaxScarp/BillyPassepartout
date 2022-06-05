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
    class OutdoorScene : Scene
    {
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
        }

        private void LoadAudio() { }

        private void LoadMap()
        {
            Map = new TmxMap("Tiled/XML/OutdoorMap.xml");
        }

        private void LoadPlayer()
        {
            Player = new Player();
            Player.Position = PlayerStartingPos;
        }

        private void LoadObjects()
        {
            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name.Contains("Door"))
                {
                    obj.OnDoorReached += DoorReached;
                }
            }
        }

        private void DoorReached(object sender)
        {
            switch (((TmxObject)sender).Name)
            {
                case "LeftLowerDoor":
                    break;
                case "LeftUpperDoor":
                    break;
                case "UpperDoor":
                    break;
                case "RightUpperDoor":
                    break;
                case "RightLowerDoor":
                    break;
                case "DungeonDoor":
                    break;
                case "WallDoor":
                    break;
                case "HomeDoor":
                    Game.HomeScene.PlayerStartingPos = new Vector2(27, 22);
                    SceneManager.LoadScene(0);
                    break;
                case "LeftHouseDoor":
                    break;
                case "CenterHouseDoor":
                    break;
                case "RightHouseDoor":
                    break;
            }
        }
    }
}
