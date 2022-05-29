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
    class TitleScene : Scene
    {
        public override void Start()
        {
            Game.Window.SetMouseVisible(true);

            LoadAssets();

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

        public override void Update() { }

        public override void Draw() { }

        private void LoadAssets() { }
    }
}
