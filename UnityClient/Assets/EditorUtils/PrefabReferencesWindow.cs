using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabReferencesWindow : EditorWindow
{
    private List<string> _references = new List<string>();

    public void SetReferences(List<string> references)
    {
        _references = references;
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab References", EditorStyles.boldLabel);

        if (_references.Count > 0)
        {
            foreach (string prefabPath in _references)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(prefabPath, GUILayout.Width(position.width - 60));
                if (GUILayout.Button("Ping", GUILayout.Width(50)))
                {
                    Object prefab = AssetDatabase.LoadAssetAtPath<Object>(prefabPath);
                    EditorGUIUtility.PingObject(prefab);
                }
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.Label("No referenced prefabs found.");
        }
    }
}