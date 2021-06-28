using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ADV
{
    [CustomEditor(typeof(DrawLine))]
    [CanEditMultipleObjects]
    public class Editor_DrawLine : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Draw"))
            {
                DrawLine DL = (DrawLine)target;
                if (!DL.PointI || !DL.PointII)
                    return;
                Undo.RegisterFullObjectHierarchyUndo(DL.gameObject, "Draw");
                Undo.RecordObject(DrawControl.GetMain(), "Draw");
                DL.Draw();
                PrefabUtility.RecordPrefabInstancePropertyModifications(DL.gameObject);
            }
        }
    }
}