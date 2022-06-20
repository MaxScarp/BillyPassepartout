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
            GfxManager.AddTexture("sword", "Assets/Objects/Sword.png");
        }

        private void LoadAudio()
        {
            AudioManager.AddClip("doorUnlocked", "Assets/Sounds/DoorUnlocked.ogg");
            AudioManager.AddClip("rotatingSword", "Assets/Sounds/SwordThrow.ogg");
        }

        private void LoadMap()
        {
            Map = new TmxMap("Tiled/XML/HomeMap.xml");
        }

        private void LoadPlayer()
        {
            Player = new Player();
            if(!IsKeyCollected)
            {
                PlayerStartingPos = Game.ScreenCenter;
            }
            Player.Position = PlayerStartingPos;
        }

        private void LoadObjects()
        {
            if(!IsKeyCollected)
            {
                Key = new Key();
                Key.IsActive = true;
                Key.Position = new Vector2(10, 2);
                Key.OnKeyCollected += KeyCollected;
            }

            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name.Contains("Door"))
                {
                    obj.OnDoorReached += DoorReached;
                    break;
                }
            }
        }

        private void KeyCollected(object sender)
        {
            Key.AudioSource.Play(AudioManager.GetClip("doorUnlocked"));

            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name == "Door")
                {
                    Map.PathfindingMap.ToggleNode(obj.X, obj.Y, 2);
                    Key.IsActive = false;
                    IsKeyCollected = true;
                    break;
                }
            }
        }

        private void DoorReached(object sender)
        {
            Game.OutdoorScene.PlayerStartingPos = new Vector2(5, 18);
            Player.RigidBody.Velocity = Vector2.Zero;
            IsPlaying = false;
        }
    }
}
