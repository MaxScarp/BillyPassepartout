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
        TmxMap map;

        public override void Start()
        {
            LoadAssets();
            LoadAudio();
            LoadMap();
            
            base.Start();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Input()
        {
            Quit();
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
            GfxManager.AddTexture("tileset", "Assets/Tileset.png");
        }

        private void LoadAudio() { }

        private void LoadMap()
        {
            map = new TmxMap("Tiled/XML/HomeMap.xml");
        }
    }
}
