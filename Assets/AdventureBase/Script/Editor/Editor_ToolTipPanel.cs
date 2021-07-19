using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ADV
{
    [CustomEditor(typeof(ToolTipPanel))]
    [CanEditMultipleObjects]
    public class Editor_ToolTipPanel : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Apply"))
            {
                ToolTipPanel TTP = (ToolTipPanel)target;
                Undo.RegisterFullObjectHierarchyUndo(TTP.gameObject, "Draw");
                TTP.Apply();
                PrefabUtility.RecordPrefabInstancePropertyModifications(TTP.gameObject);
            }
        }
    }
}
