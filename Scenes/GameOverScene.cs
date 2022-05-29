using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BillyPassepartout
{
    class GameOverScene : Scene
    {
        public override void Start()
        {
            LoadAssets();

            base.Start();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Input() { }

        public override void Update() { }

        public override void Draw() { }

        private void LoadAssets() { }
    }
}
