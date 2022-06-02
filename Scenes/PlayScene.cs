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
        private TmxMap map;
        private Player player;

        public override void Start()
        {
            LoadAssets();
            LoadAudio();
            LoadMap();
            LoadPlayer();
            
            base.Start();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Input()
        {
            Quit();
            player.Input();
        }

        public override void Update()
        {
            UpdateManager.Update();
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
            map = new TmxMap("Tiled/XML/HomeMap.xml");
        }

        private void LoadPlayer()
        {
            player = new Player();
        }
    }
}
