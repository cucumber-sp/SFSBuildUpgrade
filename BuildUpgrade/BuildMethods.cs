using SFS.Builds;
using SFS.Parts;
using SFS.Parts.Modules;
using SFS.World;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuildUpgrade
{
    public class BuildMethods
    {
        public static Vector2 MyGetSnapPosition(Vector2 position, HoldGrid hold_grid)
        {
            hold_grid.holdGrid.transform.position = position;
            ConvexPolygon[] buildColliders = hold_grid.buildGrid.buildColliders.Select((BuildGrid.PartCollider a) => a.colliders).Collapse().ToArray();
            MagnetModule[] modules = hold_grid.holdGrid.partsHolder.GetModules<MagnetModule>();
            MagnetModule[] modules2 = hold_grid.buildGrid.activeGrid.partsHolder.GetModules<MagnetModule>();
            Vector2 vector = Vector2.zero;
            float num = float.PositiveInfinity;
            if (modules.Length <= 8 && Menu.IsSnap)
            {
                MagnetModule[] array = modules;
                foreach (MagnetModule a2 in array)
                {
                    MagnetModule[] array2 = modules2;
                    foreach (MagnetModule b in array2)
                    {
                        foreach (Vector2 snapOffset in MagnetModule.GetSnapOffsets(a2, b, 0.75f))
                        {
                            if (snapOffset.sqrMagnitude < num)
                            {
                                vector = snapOffset;
                                num = snapOffset.sqrMagnitude;
                            }
                        }
                    }
                }
            }
            if (num < float.PositiveInfinity)
            {
                return position + vector;
            }
            if (SandboxSettings.main.settings.partClipping)
            {
                return position.Round(Menu.PartMovementGrid);
            }
            List<Vector2> obj = new List<Vector2>
    {
        position.Round(Menu.PartMovementGrid),
        (position + Vector2.left * 0.4f).Round(Menu.PartMovementGrid),
        (position + Vector2.right * 0.4f).Round(Menu.PartMovementGrid),
        (position + Vector2.up * 0.4f).Round(Menu.PartMovementGrid),
        (position + Vector2.down * 0.4f).Round(Menu.PartMovementGrid)
    };
            Vector2 result = position;
            float num2 = float.PositiveInfinity;
            foreach (Vector2 item in obj)
            {
                if ((item - position).sqrMagnitude < num2)
                {
                    result = item;
                    num2 = (item - position).sqrMagnitude;
                }
            }
            if (num2 < float.PositiveInfinity)
            {
                return result;
            }
            return position.Round(Menu.PartMovementGrid);
        }

        public static void RefreshPartValues()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                Menu.x_orientation_string = parts.ElementAt(0).orientation.orientation.Value.x.ToString();
                Menu.y_orientation_string = parts.ElementAt(0).orientation.orientation.Value.y.ToString();
                Menu.z_orientation_string = parts.ElementAt(0).orientation.orientation.Value.z.ToString();
                Menu.x_position_string = parts.ElementAt(0).Position.x.ToString();
                Menu.y_position_string = parts.ElementAt(0).Position.y.ToString();
                Menu.width_original_string = Math.Max(Math.Max(parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_original"), parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width")), parts.ElementAt(0).variablesModule.doubleVariables.GetValue("size")).ToString();
                Menu.width_b_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_b").ToString();
                Menu.width_a_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_a").ToString();
                Menu.height_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("height").ToString();
                return;
            }
            Menu.x_orientation_string = "";
            Menu.y_orientation_string = "";
            Menu.z_orientation_string = "";
            Menu.x_position_string = "";
            Menu.y_position_string = "";
            Menu.width_original_string = "";
            Menu.width_b_string = "";
            Menu.width_a_string = "";
            Menu.height_string = "";
        }
        public static void ApplyOrientation()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                parts.ElementAt(0).orientation.orientation.Value = new SFS.Orientation(float.Parse(Menu.x_orientation_string), float.Parse(Menu.y_orientation_string), float.Parse(Menu.z_orientation_string));
                parts.ElementAt(0).RegenerateMesh();
            }

        }
        public static void ApplyPosition()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                parts.ElementAt(0).Position = new Vector2(float.Parse(Menu.x_position_string), float.Parse(Menu.y_position_string));
                parts.ElementAt(0).RegenerateMesh();
            }

        }

        public static void ApplySize()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width", double.Parse(Menu.width_original_string));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_original", double.Parse(Menu.width_original_string));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_b", double.Parse(Menu.width_b_string));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_a", double.Parse(Menu.width_a_string));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("height", double.Parse(Menu.height_string));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("size", double.Parse(Menu.width_original_string));
                parts.ElementAt(0).RegenerateMesh();
                AdaptModule.UpdateAdaptation(parts);
            }

        }
    }
}
