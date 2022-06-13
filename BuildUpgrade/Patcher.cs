using HarmonyLib;
using SFS;
using SFS.Builds;
using SFS.Parts.Modules;
using UnityEngine;

namespace BuildUpgrade
{
    public class Classes
    {
        public static HoldGrid ThisHoldGrid;

        public static BuildGrid ThisBuildGrid;
    }

    [HarmonyPatch(typeof(HoldGrid), "GetSnapPosition_Old")]
    public class HoldGrid_GetSnapPosition_Old
    {
        [HarmonyPrefix]
        public static bool Prefix(Vector2 position, out Vector2 __state)
        {
            __state = position;
            return true;
        }
        [HarmonyPostfix]
        public static void Postfix(ref Vector2 __result, Vector2 __state)
        {
            __result = BuildMethods.MyGetSnapPosition(__state, Classes.ThisHoldGrid);
        }
    }

    [HarmonyPatch(typeof(HoldGrid), "Start")]
    public class HoldGrid_Start
    {
        [HarmonyPrefix]
        public static void Prefix(ref HoldGrid __instance)
        {
            Classes.ThisHoldGrid = __instance;
        }
    }

    [HarmonyPatch(typeof(HoldGrid), "TakePart_PickGrid")]
    class AdaptPartPicker
    {
        [HarmonyPrefix]
        static void Prefix()
        {
            BuildMethods.noAdaptOverride = true;
        }

        [HarmonyPostfix]
        static void Postfix()
        {
            BuildMethods.noAdaptOverride = false;
        }
    }

    [HarmonyPatch(typeof(BuildGrid), "Start")]
    public class BuildGrid_Start
    {
        [HarmonyPrefix]
        public static void Prefix(ref BuildGrid __instance)
        {
            Classes.ThisBuildGrid = __instance;
        }
    }

    [HarmonyPatch(typeof(AdaptModule), "Adapt")]
    public class PartGrid_Adapt
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (!Menu.isAdaptationOn && !BuildMethods.noAdaptOverride)
            {
                return false;
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(Selector), "ToggleSelected")]
    public class Selector_ToggleSelected
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            BuildMethods.RefreshPartValues();
        }
    }

    [HarmonyPatch(typeof(Selector), "DeselectAll")]
    public class Selector_DeselectAll
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            BuildMethods.RefreshPartValues();
        }
    }

    [HarmonyPatch(typeof(HoldGrid), "EndHold")]
    public class HoldGrid_EndHold
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            BuildMethods.RefreshPartValues();
        }
    }

    [HarmonyPatch(typeof(BuildMenus), "OnEmptyClick")]
    public class BuildMenus_OnEmptyClick
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            return Menu.IsDeselectOnEmptyClick;
        }
    }
}