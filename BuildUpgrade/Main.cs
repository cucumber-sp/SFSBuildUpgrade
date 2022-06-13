using HarmonyLib;
using ModLoader;
using SFS;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildUpgrade
{
    public class Main : SFSMod
    {
        public static GameObject menu = new GameObject("Cucumber Space");
        public Main() : base("BuildUpgrade", "BuildUpgrade", "Cucumber Space", "v1.3.2", "Beta 1.2")
        {
        }

        public static GameObject menuObject;

        public override void load()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            applyPatches();
            menu.AddComponent<Menu>();
            UnityEngine.Object.DontDestroyOnLoad(menu);
            Main.menuObject = menu;
        }

        public static Scene currentScene;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            currentScene = scene;
            if(scene.name == "Build_PC")
            {
                menu.SetActive(true);

                foreach (string key in Base.partsLoader.colorTextures.Keys)
                {
                    Base.partsLoader.colorTextures[key].tags = new string[] { "tank", "cone", "fairing" };
                }

                foreach (string key in Base.partsLoader.shapeTextures.Keys)
                {
                    Base.partsLoader.shapeTextures[key].tags = new string[] { "tank", "cone", "fairing" };
                }
            }
            else
            {
                menu.SetActive(false);
            }
            
        }

        public override void unload()
        {
        }

        private void applyPatches() // is not necesary do whit static function
        {
            Harmony harmony = new Harmony("com.CucumberSpace.BuildUpgrade");
            harmony.PatchAll();
        }
    }
}
