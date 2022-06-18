using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace BillyPassepartout
{
    static class Game
    {
        public static Window Window;
        public static HomeScene HomeScene;
        public static OutdoorScene OutdoorScene;
        public static DungeonBeforeScene DungeonBeforeScene;
        public static DungeonAfterScene DungeonAfterScene;
        public static AudioSource BackgroundAudioSource;
        public static AudioClip backgroundClip;

        public static float DeltaTime { get { return Window.DeltaTime; } }

        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static Vector2 ScreenCenter { get; private set; }
        public static float HalfDiagonalSquared { get { return ScreenCenter.LengthSquared; } }
        public static Vector2 MousePos 
        {
            get
            {
                if(Window.MousePosition.X >= 0 && Window.MousePosition.X < Game.Window.OrthoWidth && Window.MousePosition.Y >= 0 && Window.MousePosition.Y < Game.Window.OrthoHeight)
                {
                    return new Vector2(Window.MousePosition.X, Window.MousePosition.Y);
                }

                return Vector2.Zero;
            }
        }

        public static void Init()
        {
            Window = new Window(1024, 768, "BILLY PASSEPARTOUT");
            Window.SetVSync(false);
            Window.SetDefaultViewportOrthographicSize(24);

            OptimalScreenHeight = 384;
            UnitSize = Window.Height / Window.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight;

            ScreenCenter = new Vector2(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);

            HomeScene = new HomeScene();
            OutdoorScene = new OutdoorScene();
            DungeonBeforeScene = new DungeonBeforeScene();
            DungeonAfterScene = new DungeonAfterScene();

            BackgroundAudioSource = new AudioSource();
            backgroundClip = new AudioClip("Assets/Sounds/Background.ogg");
            BackgroundAudioSource.Play(backgroundClip, true);

            PersistentData.Init();

            SceneManager.Start();
        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }

        public static void Play()
        {
            while (Window.IsOpened)
            {
                // Show FPS on Window Title Bar
                Window.SetTitle($"FPS: {1f / Window.DeltaTime}"); //DEBUG

                // INPUT
                SceneManager.CurrentScene.Input();

                // UPDATE
                SceneManager.Update();
                SceneManager.CurrentScene.Update();

                // DRAW
                SceneManager.CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}
