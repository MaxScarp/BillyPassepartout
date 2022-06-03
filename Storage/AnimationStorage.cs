using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    /// <summary>
    /// Class that is used as a storage of all animations
    /// </summary>
    static class AnimationStorage
    {
        private static int frameW = 16;
        private static int frameH = 16;
        private static int fps = 15;

        public static void LoadPlayerAnimations()
        {
            //Dog Attack
            GfxManager.AddAnimation("attackD", fps, 2, frameW, frameH, 1, 1);
            GfxManager.AddAnimation("attackR", fps, 2, frameW, frameH, 1, 2);
            GfxManager.AddAnimation("attackU", fps, 2, frameW, frameH, 1, 3);
            //Dog Die                          
            GfxManager.AddAnimation("die", fps, 2, frameW, frameH, 1, 4);
            //Dog Hurt
            GfxManager.AddAnimation("hurtD", fps, 1, frameW, frameH, 1, 5);
            GfxManager.AddAnimation("hurtR", fps, 1, frameW, frameH, 1, 6);
            GfxManager.AddAnimation("hurtU", fps, 1, frameW, frameH, 1, 7);
            //Dog Roll
            GfxManager.AddAnimation("rollD", fps, 3, frameW, frameH, 1, 8);
            GfxManager.AddAnimation("rollR", fps, 3, frameW, frameH, 1, 9);
            GfxManager.AddAnimation("rollU", fps, 3, frameW, frameH, 1, 10);
            //Dog Push
            GfxManager.AddAnimation("pushD", fps, 3, frameW, frameH, 1, 11);
            GfxManager.AddAnimation("pushR", fps, 3, frameW, frameH, 1, 12);
            GfxManager.AddAnimation("pushU", fps, 3, frameW, frameH, 1, 13);
            //Dog Idle
            GfxManager.AddAnimation("idleD", fps, 1, frameW, frameH, 1, 14);
            GfxManager.AddAnimation("idleR", fps, 1, frameW, frameH, 1, 15);
            GfxManager.AddAnimation("idleU", fps, 1, frameW, frameH, 1, 16);
            //Dog Walk                          
            GfxManager.AddAnimation("walkD", fps, 4, frameW, frameH, 1, 17);
            GfxManager.AddAnimation("walkR", fps, 4, frameW, frameH, 1, 18);
            GfxManager.AddAnimation("walkU", fps, 4, frameW, frameH, 1, 19);
        }

        public static void LoadEnemyAnimations() { }
    }
}
