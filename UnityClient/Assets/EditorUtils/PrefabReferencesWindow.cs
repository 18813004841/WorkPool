using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabReferencesWindow : EditorWindow
{
    private List<string> _references = new List<string>();

    private void OnGUI()
    {
        GUILayout.Label("Prefab References", EditorStyles.boldLabel);

        if (FindPrefabReferences._isSearching)
        {
            if (GUILayout.Button("Stop Search"))
            {
                FindPrefabReferences.StopSearch();
            }
        }

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
        else if (!FindPrefabReferences._isSearching)
        {
            GUILayout.Label("No references found.");
        }
    }

    private void OnEnable()
    {
        FindPrefabReferences._referencedPrefabs.Clear();
        FindPrefabReferences._isSearching = true;

        EditorApplication.update += UpdateWindow;
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateWindow;
    }

    private void UpdateWindow()
    {
        if (!FindPrefabReferences._isSearching && _references.Count == 0)
        {
            string message = "No references found.";

            if (FindPrefabReferences._referencedPrefabs.Count > 0)
            {
                message = string.Format("{0} references found.", FindPrefabReferences._referencedPrefabs.Count);

                foreach (string prefabPath in FindPrefabReferences._referencedPrefabs)
                {
                    _references.Add(prefabPath);
                }
            }

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Prefab References", message, "OK");
            Close();
        }
        else
        {
            Repaint();
        }
    }
}