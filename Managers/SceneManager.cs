using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    static class SceneManager
    {
        private static List<Scene> scenes;

        private static int currentSceneIndex;
        private static bool manualLoad;

        public static Scene CurrentScene { get; private set; }

        static SceneManager()
        {
            scenes = new List<Scene>();
            currentSceneIndex = 0;
            manualLoad = false;
        }

        public static void Start()
        {
            CurrentScene = scenes[currentSceneIndex];
            CurrentScene.Start();
        }

        public static void Update()
        {
            if (!CurrentScene.IsPlaying && !manualLoad)
            {
                if(++currentSceneIndex >= scenes.Count)
                {
                    currentSceneIndex = 0;
                }

                CurrentScene.OnExit();
                CurrentScene = scenes[currentSceneIndex];
                CurrentScene.Start();
            }
        }

        public static void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public static void LoadScene(int sceneIndex)
        {
            manualLoad = true;
            CurrentScene.OnExit();
            CurrentScene = scenes[sceneIndex];
            CurrentScene.Start();
            manualLoad = false;
        }
    }
}
