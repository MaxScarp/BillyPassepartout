using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    static class PersistentData
    {
        public static bool IsDungeonButtonPressed;
        public static bool IsWallKeyCollected;
        public static bool IsRedKeyCollected;
        public static bool IsBlueKeyCollected;
        public static bool IsSkullKeyCollected;
        public static bool IsSwordCollected;

        public static int PlayerLives;

        public static void Init()
        {
            IsDungeonButtonPressed = false;
            IsWallKeyCollected = false;
            IsRedKeyCollected = false;
            IsBlueKeyCollected = false;
            IsSkullKeyCollected = false;
            IsSwordCollected = false;

            PlayerLives = 3;
        }
    }
}
