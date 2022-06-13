using SFS.Builds;
using SFS.Parts;
using SFS.Parts.Modules;
using SFS.World;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace BuildUpgrade
{
    public class BuildMethods
    {
        public static bool noAdaptOverride = false;
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
                List<Vector2> allSnapOffsets = MagnetModule.GetAllSnapOffsets(modules, modules2, 0.75f);
			if (allSnapOffsets.Count > 0)
			{
				allSnapOffsets.Sort((Vector2 a, Vector2 b) => a.sqrMagnitude.CompareTo(b.sqrMagnitude));
				foreach (Vector2 item in allSnapOffsets)
				{
                        Vector2 transformVector = hold_grid.holdGrid.transform.position;
                        hold_grid.transform.position = position;
                        bool collisionResult = Polygon.Intersect(Part_Utility.GetBuildColliderPolygons(hold_grid.holdGrid.partsHolder.parts.ToArray()).normal, buildColliders, -0.08f);
                        hold_grid.holdGrid.transform.position = transformVector;
                        if (!collisionResult)
					{
						return position + item;
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
                Menu.x_orientation_string = parts.ElementAt(0).orientation.orientation.Value.x.ToString(CultureInfo.InvariantCulture);
                Menu.y_orientation_string = parts.ElementAt(0).orientation.orientation.Value.y.ToString(CultureInfo.InvariantCulture);
                Menu.z_orientation_string = parts.ElementAt(0).orientation.orientation.Value.z.ToString(CultureInfo.InvariantCulture);
                Menu.x_position_string = parts.ElementAt(0).Position.x.ToString(CultureInfo.InvariantCulture);
                Menu.y_position_string = parts.ElementAt(0).Position.y.ToString(CultureInfo.InvariantCulture);
                Menu.width_original_string = Math.Max(Math.Max(parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_original"), parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width")), parts.ElementAt(0).variablesModule.doubleVariables.GetValue("size")).ToString(CultureInfo.InvariantCulture);
                Menu.width_b_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_b").ToString(CultureInfo.InvariantCulture);
                Menu.width_a_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("width_a").ToString(CultureInfo.InvariantCulture);
                Menu.height_string = parts.ElementAt(0).variablesModule.doubleVariables.GetValue("height").ToString(CultureInfo.InvariantCulture);
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
                parts.ElementAt(0).orientation.orientation.Value = new Orientation(float.Parse(Menu.x_orientation_string, CultureInfo.InvariantCulture), float.Parse(Menu.y_orientation_string, CultureInfo.InvariantCulture), float.Parse(Menu.z_orientation_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).RegenerateMesh();
            }

        }
        public static void ApplyPosition()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                parts.ElementAt(0).Position = new Vector2(float.Parse(Menu.x_position_string, CultureInfo.InvariantCulture), float.Parse(Menu.y_position_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).RegenerateMesh();
            }

        }

        public static void ApplySize()
        {
            Part[] parts = BuildManager.main.buildGrid.GetSelectedParts();
            if (parts.Length > 0)
            {
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width", double.Parse(Menu.width_original_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_original", double.Parse(Menu.width_original_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_b", double.Parse(Menu.width_b_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("width_a", double.Parse(Menu.width_a_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("height", double.Parse(Menu.height_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).variablesModule.doubleVariables.SetValue("size", double.Parse(Menu.width_original_string, CultureInfo.InvariantCulture));
                parts.ElementAt(0).RegenerateMesh();
                AdaptModule.UpdateAdaptation(parts);
            }

        }
    }
}
