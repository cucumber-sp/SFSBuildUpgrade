using UnityEngine;
using ModLoader;

namespace BuildUpgrade
{
    public class Menu : MonoBehaviour
    {
        private bool isPartMenuOn = false;

        private bool isBuildMenuOn = false;

        public Rect ModSelectWindowRect = new Rect(100f, 20f, 200f, 100f);

        public Rect ModPartWindowRect = new Rect(100f, 140f, 250f, 460f);

        public Rect ModBuildWindowRect = new Rect(100f, 620f, 200f, 180f);

        public static bool isAdaptationOn = true;

        public static bool IsSnap = true;

        public static bool IsDeselectOnEmptyClick = true;

        private string PartMovementGrid_String = "0,5";

        public static float PartMovementGrid = 0.5f;

        public static string x_orientation_string;

        public static string y_orientation_string;

        public static string z_orientation_string;

        public static string x_position_string;

        public static string y_position_string;

        public static string width_original_string;

        public static string width_b_string;

        public static string width_a_string;

        public static string height_string;

        void OnGUI()
        {
            if (Helper.currentScene == scene.Build)
            {
                ModSelectWindowRect = GUI.Window(GUIUtility.GetControlID(FocusType.Passive), ModSelectWindowRect, ModGUIMainWindow, "Cucumber's build plugin");
                if (isPartMenuOn)
                {
                    ModPartWindowRect = GUI.Window(GUIUtility.GetControlID(FocusType.Passive), ModPartWindowRect, ModGUIPartWindow, "Part Parameters");
                }
                if (isBuildMenuOn)
                {
                    ModBuildWindowRect = GUI.Window(GUIUtility.GetControlID(FocusType.Passive), ModBuildWindowRect, ModGUIBuildWindow, "Build Parameters");
                }
            }
        }
        void ModGUIMainWindow(int windowID)
        {

            isPartMenuOn = GUI.Toggle(new Rect(20f, 30f, 150f, 20f), isPartMenuOn, " Part Parameters");
            isBuildMenuOn = GUI.Toggle(new Rect(20f, 60f, 150f, 20f), isBuildMenuOn, " Build Parameters");
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));

        }

        void ModGUIBuildWindow(int windowID)
        {

            isAdaptationOn = GUI.Toggle(new Rect(30f, 30f, 150f, 20f), isAdaptationOn, " Part Adaptation");
            IsSnap = GUI.Toggle(new Rect(30f, 60f, 150f, 20f), IsSnap, " Part Magnetic");
            IsDeselectOnEmptyClick = GUI.Toggle(new Rect(30f, 90f, 150f, 20f), IsDeselectOnEmptyClick, " Deselect On Click");
            GUI.Label(new Rect(30f, 120f, 65f, 20f), "Movement");
            PartMovementGrid_String = GUI.TextField(new Rect(105f, 120f, 60f, 20f), PartMovementGrid_String);
            if (GUI.Button(new Rect(20f, 150f, 160f, 20f), "Submit"))
            {
                if ((PartMovementGrid_String == "") | (float.Parse(PartMovementGrid_String) == 0f) | float.IsNaN(float.Parse(PartMovementGrid_String)))
                {
                    PartMovementGrid_String = "0,5";
                }
                PartMovementGrid = float.Parse(PartMovementGrid_String);
            }
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));

        }

        void ModGUIPartWindow(int windowID)
        {

            GUI.Box(new Rect(20f, 30f, 210f, 120f), "Orientation Values");
            GUI.Label(new Rect(30f, 60f, 20f, 20f), "X");
            x_orientation_string = GUI.TextField(new Rect(85f, 60f, 100f, 20f), x_orientation_string);
            if (GUI.Button(new Rect(60f, 60f, 20f, 20f), "<"))
            {
                x_orientation_string = (float.Parse(x_orientation_string) - 0.1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 60f, 20f, 20f), ">"))
            {
                x_orientation_string = (float.Parse(x_orientation_string) + 0.1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 90f, 20f, 20f), "Y");
            y_orientation_string = GUI.TextField(new Rect(85f, 90f, 100f, 20f), y_orientation_string);
            if (GUI.Button(new Rect(60f, 90f, 20f, 20f), "<"))
            {
                y_orientation_string = (float.Parse(y_orientation_string) - 0.1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 90f, 20f, 20f), ">"))
            {
                y_orientation_string = (float.Parse(y_orientation_string) + 0.1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 120f, 20f, 20f), "Z");
            z_orientation_string = GUI.TextField(new Rect(85f, 120f, 100f, 20f), z_orientation_string);
            if (GUI.Button(new Rect(60f, 120f, 20f, 20f), "<"))
            {
                z_orientation_string = (float.Parse(z_orientation_string) - 1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 120f, 20f, 20f), ">"))
            {
                z_orientation_string = (float.Parse(z_orientation_string) + 1f).ToString();
                BuildMethods.ApplyOrientation();
                BuildMethods.RefreshPartValues();
            }
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
            GUI.Box(new Rect(20f, 160f, 210f, 90f), "Position Values");
            GUI.Label(new Rect(30f, 190f, 20f, 20f), "X");
            x_position_string = GUI.TextField(new Rect(85f, 190f, 100f, 20f), x_position_string);
            if (GUI.Button(new Rect(60f, 190f, 20f, 20f), "<"))
            {
                x_position_string = (float.Parse(x_position_string) - 0.1f).ToString();
                BuildMethods.ApplyPosition();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 190f, 20f, 20f), ">"))
            {
                x_position_string = (float.Parse(x_position_string) + 0.1f).ToString();
                BuildMethods.ApplyPosition();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 220f, 20f, 20f), "Y");
            y_position_string = GUI.TextField(new Rect(85f, 220f, 100f, 20f), y_position_string);
            if (GUI.Button(new Rect(60f, 220f, 20f, 20f), "<"))
            {
                y_position_string = (float.Parse(y_position_string) - 0.1f).ToString();
                BuildMethods.ApplyPosition();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 220f, 20f, 20f), ">"))
            {
                y_position_string = (float.Parse(y_position_string) + 0.1f).ToString();
                BuildMethods.ApplyPosition();
                BuildMethods.RefreshPartValues();
            }
            GUI.Box(new Rect(20f, 260f, 210f, 150f), "Size Values");
            GUI.Label(new Rect(30f, 290f, 50f, 20f), "Width");
            width_original_string = GUI.TextField(new Rect(105f, 290f, 80f, 20f), width_original_string);
            if (GUI.Button(new Rect(80f, 290f, 20f, 20f), "<"))
            {
                width_original_string = (float.Parse(width_original_string) - 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 290f, 20f, 20f), ">"))
            {
                width_original_string = (float.Parse(width_original_string) + 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 320f, 50f, 20f), "Top");
            width_b_string = GUI.TextField(new Rect(105f, 320f, 80f, 20f), width_b_string);
            if (GUI.Button(new Rect(80f, 320f, 20f, 20f), "<"))
            {
                width_b_string = (float.Parse(width_b_string) - 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 320f, 20f, 20f), ">"))
            {
                width_b_string = (float.Parse(width_b_string) + 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 350f, 50f, 20f), "Bottom");
            width_a_string = GUI.TextField(new Rect(105f, 350f, 80f, 20f), width_a_string);
            if (GUI.Button(new Rect(80f, 350f, 20f, 20f), "<"))
            {
                width_a_string = (float.Parse(width_a_string) - 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 350f, 20f, 20f), ">"))
            {
                width_a_string = (float.Parse(width_a_string) + 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            GUI.Label(new Rect(30f, 380f, 50f, 25f), "Height");
            height_string = GUI.TextField(new Rect(105f, 380f, 80f, 20f), height_string);
            if (GUI.Button(new Rect(80f, 380f, 20f, 20f), "<"))
            {
                height_string = (float.Parse(height_string) - 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(190f, 380f, 20f, 20f), ">"))
            {
                height_string = (float.Parse(height_string) + 0.1f).ToString();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
            if (GUI.Button(new Rect(20f, 420f, 210f, 30f), "Submit"))
            {
                BuildMethods.ApplyOrientation();
                BuildMethods.ApplyPosition();
                BuildMethods.ApplySize();
                BuildMethods.RefreshPartValues();
            }
        }
    }
}
