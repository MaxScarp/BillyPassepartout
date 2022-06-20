using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    class DungeonBeforeScene : Scene
    {
        private AudioSource buttonAudioSource;

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
            Sword = null;
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
            buttonAudioSource = new AudioSource();
            AudioManager.AddClip("buttonPressed", "Assets/Sounds/ButtonPressed.ogg");
            AudioManager.AddClip("swordCollected", "Assets/Sounds/SwordReached.ogg");
            AudioManager.AddClip("rotatingSword", "Assets/Sounds/SwordThrow.ogg");
        }

        private void LoadMap()
        {
            Map = new TmxMap("Tiled/XML/DungeonBefore.xml");
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
                enemy.Position = new Vector2(5, 6);

                Enemies.Add(enemy);
            }
        }

        private void LoadObjects()
        {
            Key = new Key();
            Key.IsActive = true;
            Key.Position = new Vector2(18, 14);

            if(!PersistentData.IsSwordCollected)
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
                else if(obj.Name.Contains("Button"))
                {
                    obj.OnButtonReached += ButtonReached;
                }
                else if(obj.Name.Contains("Trap"))
                {
                    obj.OnTrapReached += TrapReached;
                }
            }
        }

        private void SwordCollected(object sender)
        {
            Sword.AudioSource.Play(AudioManager.GetClip("swordCollected"));
            Sword.IsActive = false;
            PersistentData.IsSwordCollected = true;
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

        private void ButtonReached(object sender)
        {
            buttonAudioSource.Play(AudioManager.GetClip("buttonPressed"));
            PersistentData.IsDungeonButtonPressed = true;
            Game.DungeonAfterScene.PlayerStartingPos = new Vector2(4, 6);
            Player.RigidBody.Velocity = Vector2.Zero;
            IsPlaying = false;
        }

        private void TrapReached(object sender)
        {
            Player.Lives--;
            Player.Agent.ResetPath();
        }
    }
}
