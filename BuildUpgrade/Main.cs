using HarmonyLib;
using ModLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildUpgrade
{
    public class Main : SFSMod
    {
        public static GameObject menuObject;
        public string getModAuthor()
        {
            return "Cucumber Space";
        }

        public string getModName()
        {
            return "BuildUpgrade";
        }

        public void load()
        {
            applyPatches(); // apply my harmony patchers
            Loader.modLoader.suscribeOnChangeScene(this.OnSceneLoaded); // this suscribe a function to listen if the scene change

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Build_PC") // when is bulding area create a game object whit Menu class
            {
                GameObject menu = new GameObject("Cucumber Space");
                menu.AddComponent<Menu>();
                UnityEngine.Object.DontDestroyOnLoad(menu);
                menu.SetActive(true);
                Main.menuObject = menu;
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

        public void unload()
        {
            throw new System.NotImplementedException();
        }

        public void applyPatches() // is not necesary do whit static function
        {
            Harmony harmony = new Harmony("com.CucumberSpace.BuildUpgrade");
            harmony.PatchAll();
        }
    }
}
