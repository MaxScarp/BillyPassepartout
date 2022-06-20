using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    class DungeonAfterScene : Scene
    {
        public override void Start()
        {
            LoadAssets();
            LoadAudio();
            LoadMap();
            LoadPlayer();
            LoadEnemies();
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

            //Enemy
            GfxManager.AddTexture("ghost", "Assets/Enemy/Ghost.png");

            //Objects
            GfxManager.AddTexture("key", "Assets/Objects/SkullKey.png");
            GfxManager.AddTexture("sword", "Assets/Objects/Sword.png");
        }

        private void LoadAudio()
        {
            AudioManager.AddClip("doorUnlocked", "Assets/Sounds/DoorUnlocked.ogg");
            AudioManager.AddClip("swordCollected", "Assets/Sounds/SwordReached.ogg");
            AudioManager.AddClip("rotatingSword", "Assets/Sounds/SwordThrow.ogg");
        }

        private void LoadMap()
        {
            Map = new TmxMap("Tiled/XML/DungeonAfter.xml");
        }

        private void LoadPlayer()
        {
            Player = new Player();
            Player.Position = PlayerStartingPos;
        }

        private void LoadEnemies()
        {
            if(!PersistentData.IsWallKeyCollected)
            {
                Enemy enemy = new Enemy();
                enemy.Position = new Vector2(12, 9);

                Enemies.Add(enemy);
            }
        }

        private void LoadObjects()
        {
            if (!PersistentData.IsWallKeyCollected)
            {
                Key = new Key();
                Key.IsActive = true;
                Key.Position = new Vector2(18, 14);
                Key.OnKeyCollected += KeyCollected;
            }

            if (!PersistentData.IsSwordCollected)
            {
                Sword = new Sword();
                Sword.IsActive = true;
                Sword.Position = new Vector2(8, 16);
                Sword.OnSwordCollected += SwordCollected;
            }

            foreach (TmxObject obj in Map.ObjectsLayer.Objects)
            {
                if (obj.Name.Contains("Door"))
                {
                    obj.OnDoorReached += DoorReached;
                }
            }
        }

        private void SwordCollected(object sender)
        {
            Sword.AudioSource.Play(AudioManager.GetClip("swordCollected"));
            Sword.IsActive = false;
            PersistentData.IsSwordCollected = true;
        }

        private void KeyCollected(object sender)
        {
            Key.AudioSource.Play(AudioManager.GetClip("doorUnlocked"));
            Key.IsActive = false;
            PersistentData.IsWallKeyCollected = true;
        }

        private void DoorReached(object sender)
        {
            Player.RigidBody.Velocity = Vector2.Zero;

            switch (((TmxObject)sender).Name)
            {
                case "OutsideDoor":
                    Game.OutdoorScene.PlayerStartingPos = new Vector2(25, 20);
                    SceneManager.LoadScene(1);
                    break;
                case "LeftDoor":
                    Player.Agent.ResetPath();
                    Player.Position = new Vector2(17, 16);
                    break;
                case "RightDoor":
                    Player.Agent.ResetPath();
                    Player.Position = new Vector2(10, 16);
                    break;
            }
        }
    }
}
