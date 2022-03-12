using HarmonyLib;
using ModLoader;
using SFS;
using System;
using UnityEngine;

namespace BuildUpgrade
{
    public class Main : SFSMod
    {
        public static GameObject menu = new GameObject("Cucumber Space");
        public Main() : base("BuildUpgrade", "BuildUpgrade", "Cucumber Space", "v1.1.x", "Beta 1.1")
        {
        }

        public static GameObject menuObject;

        public override void load()
        {
            Helper.OnBuildSceneLoaded += Helper_OnBuildSceneLoaded;
            applyPatches();
            menu.AddComponent<Menu>();
            UnityEngine.Object.DontDestroyOnLoad(menu);
            Main.menuObject = menu;
        }


        private void Helper_OnBuildSceneLoaded(object sender, EventArgs _)
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
