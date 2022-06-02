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
        private static int fps = 10;

        public static void LoadPlayerAnimations()
        {
            //Dog Attack
            GfxManager.AddAnimation("AttackD", fps, 2, frameW, frameH, 1, 1);
            GfxManager.AddAnimation("AttackR", fps, 2, frameW, frameH, 1, 2);
            GfxManager.AddAnimation("AttackU", fps, 2, frameW, frameH, 1, 3);
            //Dog Die                          
            GfxManager.AddAnimation("Die", fps, 2, frameW, frameH, 1, 4);
            //Dog Hurt
            GfxManager.AddAnimation("HurtD", fps, 1, frameW, frameH, 1, 5);
            GfxManager.AddAnimation("HurtR", fps, 1, frameW, frameH, 1, 6);
            GfxManager.AddAnimation("HurtU", fps, 1, frameW, frameH, 1, 7);
            //Dog Roll
            GfxManager.AddAnimation("RollD", fps, 3, frameW, frameH, 1, 8);
            GfxManager.AddAnimation("RollR", fps, 3, frameW, frameH, 1, 9);
            GfxManager.AddAnimation("RollU", fps, 3, frameW, frameH, 1, 10);
            //Dog Push
            GfxManager.AddAnimation("PushD", fps, 3, frameW, frameH, 1, 11);
            GfxManager.AddAnimation("PushR", fps, 3, frameW, frameH, 1, 12);
            GfxManager.AddAnimation("PushU", fps, 3, frameW, frameH, 1, 13);
            //Dog Idle
            GfxManager.AddAnimation("IdleD", fps, 1, frameW, frameH, 1, 14);
            GfxManager.AddAnimation("IdleR", fps, 1, frameW, frameH, 1, 15);
            GfxManager.AddAnimation("IdleU", fps, 1, frameW, frameH, 1, 16);
            //Dog Walk                          
            GfxManager.AddAnimation("WalkD", fps, 4, frameW, frameH, 1, 17);
            GfxManager.AddAnimation("WalkR", fps, 4, frameW, frameH, 1, 18);
            GfxManager.AddAnimation("WalkU", fps, 4, frameW, frameH, 1, 19);
        }

        public static void LoadEnemyAnimations() { }
    }
}
