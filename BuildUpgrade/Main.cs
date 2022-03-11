using HarmonyLib;
using ModLoader;
using SFS;
using System;
using UnityEngine;

namespace BuildUpgrade
{
    public class Main : SFSMod
    {
        public Main() : base("BuildUpgrade", "BuildUpgrade", "Cucumber Space", "v1.1.x", "Beta 1.1")
        {
        }

        public static GameObject menuObject;

        public override void load()
        {
            Helper.OnBuildSceneLoaded += Helper_OnBuildSceneLoaded;
            applyPatches();
        }


        private void Helper_OnBuildSceneLoaded(object sender, EventArgs _)
        {
            if (ModLoader.Helper.currentScene == scene.Build) // when is bulding area create a game object whit Menu class
            {
                GameObject menu = new GameObject("Cucumber Space");
                menu.AddComponent<Menu>();
                UnityEngine.Object.DontDestroyOnLoad(menu);
                menu.SetActive(true);
                Main.menuObject = menu;

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
                if (menuObject != null) // if there is a menuObject create 
                {
                    UnityEngine.Object.Destroy(menuObject); // destroy them
                    menuObject = null;
                }

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
