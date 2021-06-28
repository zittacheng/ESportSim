using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ADV
{
    [CustomEditor(typeof(DrawPoint))]
    [CanEditMultipleObjects]
    public class Editor_DrawPoint : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Create"))
            {
                if (targets.Length != 2)
                    return;
                DrawPoint DPI = (DrawPoint)targets[0];
                DrawPoint DPII = (DrawPoint)targets[1];
                DrawControl DC = DrawControl.GetMain();
                Undo.RecordObject(DC, "Draw");
                GameObject G = (GameObject)PrefabUtility.InstantiatePrefab(DC.LinePrefab.gameObject, DPI.transform.parent);
                Undo.RegisterCreatedObjectUndo(G, "Draw");
                DrawLine DL = G.GetComponent<DrawLine>();
                DL.PointI = DPI.gameObject;
                DL.PointII = DPII.gameObject;
                string a = DPI.gameObject.name.Substring(4, DPI.gameObject.name.Length - 5);
                string b = DPII.gameObject.name.Substring(4, DPI.gameObject.name.Length - 5);
                DL.gameObject.name = "Line (" + a + "-" + b + ")";
                Undo.RegisterFullObjectHierarchyUndo(DL.gameObject, "Draw");
                DL.Draw();
                PrefabUtility.RecordPrefabInstancePropertyModifications(DL.gameObject);
            }
            if (GUILayout.Button("Change"))
            {
                DrawPoint DP = (DrawPoint)target;
                List<DrawLine> DLs = new List<DrawLine>();
                DrawControl DC = DrawControl.GetMain();
                Undo.RecordObject(DC, "Draw");
                DC.UpdateLines();
                for (int i = DC.Lines.Count - 1; i >= 0; i--)
                {
                    if (DC.Lines[i].PointI == DP.gameObject || DC.Lines[i].PointII == DP.gameObject)
                    {
                        ChangeLine(DC.Lines[i]);
                        DLs.Add(DC.Lines[i]);
                    }
                }
                foreach (DrawLine dl in DLs)
                    PrefabUtility.RecordPrefabInstancePropertyModifications(dl.gameObject);
            }
        }

        public void ChangeLine(DrawLine DL)
        {
            if (!DL.PointI || !DL.PointII)
                return;
            Undo.RegisterFullObjectHierarchyUndo(DL.gameObject, "Draw");
            DL.Draw();
        }
    }
}