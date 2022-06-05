using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyPassepartout
{
    static class SceneManager
    {
        private static int currentSceneIndex;
        private static bool manualLoad;

        public static Scene CurrentScene { get; private set; }
        public static List<Scene> Scenes { get; private set; }

        static SceneManager()
        {
            Scenes = new List<Scene>();
            currentSceneIndex = 0;
            manualLoad = false;
        }

        public static void Start()
        {
            CurrentScene = Scenes[currentSceneIndex];
            CurrentScene.Start();
        }

        public static void Update()
        {
            if (!CurrentScene.IsPlaying && !manualLoad)
            {
                if(++currentSceneIndex >= Scenes.Count)
                {
                    currentSceneIndex = 0;
                }

                CurrentScene.OnExit();
                CurrentScene = Scenes[currentSceneIndex];
                CurrentScene.Start();
            }
        }

        public static void AddScene(Scene scene)
        {
            Scenes.Add(scene);
        }

        public static void LoadScene(int sceneIndex)
        {
            manualLoad = true;
            CurrentScene.OnExit();
            currentSceneIndex = sceneIndex;
            CurrentScene = Scenes[currentSceneIndex];
            CurrentScene.Start();
            manualLoad = false;
        }

        public static Scene GetNextScene()
        {
            return Scenes[currentSceneIndex + 1] != null ? Scenes[currentSceneIndex + 1] : null;
        }
    }
}
