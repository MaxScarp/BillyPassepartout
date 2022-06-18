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
        private static int fps = 7;

        public static void LoadPlayerAnimations()
        {
            //Dog Attack
            GfxManager.AddAnimation("dogAttackD", fps, 2, frameW, frameH, 1, 1, false);
            GfxManager.AddAnimation("dogAttackR", fps, 2, frameW, frameH, 1, 2, false);
            GfxManager.AddAnimation("dogAttackU", fps, 2, frameW, frameH, 1, 3, false);
            //Dog Die                          
            GfxManager.AddAnimation("dogDie", fps, 2, frameW, frameH, 1, 4, false);
            //Dog Hurt
            GfxManager.AddAnimation("dogHurtD", fps, 1, frameW, frameH, 1, 5, false);
            GfxManager.AddAnimation("dogHurtR", fps, 1, frameW, frameH, 1, 6, false);
            GfxManager.AddAnimation("dogHurtU", fps, 1, frameW, frameH, 1, 7, false);
            //Dog Roll               
            GfxManager.AddAnimation("dogRollD", fps, 3, frameW, frameH, 1, 8, false);
            GfxManager.AddAnimation("dogRollR", fps, 3, frameW, frameH, 1, 9, false);
            GfxManager.AddAnimation("dogRollU", fps, 3, frameW, frameH, 1, 10, false);
            //Dog Push               
            GfxManager.AddAnimation("dogPushD", fps, 3, frameW, frameH, 1, 11, false);
            GfxManager.AddAnimation("dogPushR", fps, 3, frameW, frameH, 1, 12, false);
            GfxManager.AddAnimation("dogPushU", fps, 3, frameW, frameH, 1, 13, false);
            //Dog Idle               
            GfxManager.AddAnimation("dogIdleD", fps, 1, frameW, frameH, 1, 14);
            GfxManager.AddAnimation("dogIdleR", fps, 1, frameW, frameH, 1, 15);
            GfxManager.AddAnimation("dogIdleU", fps, 1, frameW, frameH, 1, 16);
            //Dog Walk                          
            GfxManager.AddAnimation("dogWalkD", fps, 4, frameW, frameH, 1, 17);
            GfxManager.AddAnimation("dogWalkR", fps, 4, frameW, frameH, 1, 18);
            GfxManager.AddAnimation("dogWalkU", fps, 4, frameW, frameH, 1, 19);
        }

        public static void LoadEnemyAnimations()
        {
            //Ghost Idle
            GfxManager.AddAnimation("ghostIdleD", fps, 1, frameW, frameH, 1, 1);
            //Ghost Walk
            GfxManager.AddAnimation("ghostWalkR", fps, 4, frameW, frameH, 1, 2);
            //Ghost Hurt
            GfxManager.AddAnimation("ghostHurtR", fps, 1, frameW, frameH, 1, 3, false);
            //Ghost Die
            GfxManager.AddAnimation("ghostDie", fps, 2, frameW, frameH, 1, 4, false);
        }
    }
}