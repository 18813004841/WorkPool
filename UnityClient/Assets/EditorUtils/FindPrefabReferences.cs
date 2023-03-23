using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class FindPrefabReferences : EditorWindow
{
    private static Object _selectedPrefab;
    private static List<string> _referencedPrefabs = new List<string>();

    [MenuItem("Assets/Custom/Find Prefab References", true)]
    private static bool ValidateFindPrefabReferences()
    {
        return Selection.activeObject != null && Selection.activeObject is GameObject;
    }

    [MenuItem("Assets/Custom/Find Prefab References")]
    private static void FindPrefabReferencesFunc()
    {
        _referencedPrefabs.Clear();
        _selectedPrefab = Selection.activeObject;

        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        EditorUtility.DisplayProgressBar("Find Prefab References", "Finding referenced prefabs...", 0f);

        for (int i = 0; i < guids.Length; i++)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab != null)
            {
                string[] dependencies = AssetDatabase.GetDependencies(prefabPath);

                for (int j = 0; j < dependencies.Length; j++)
                {
                    if (dependencies[j] == AssetDatabase.GetAssetPath(_selectedPrefab))
                    {
                        _referencedPrefabs.Add(prefabPath);
                        break;
                    }
                }
            }

            float progress = (float)i / guids.Length;
            EditorUtility.DisplayProgressBar("Find Prefab References", "Finding referenced prefabs...", progress);
        }

        EditorUtility.ClearProgressBar();

        if (_referencedPrefabs.Count > 0)
        {
            PrefabReferencesWindow window = GetWindow<PrefabReferencesWindow>("Prefab References");
            window.Show();
            window.SetReferences(_referencedPrefabs);
        }
        else
        {
            EditorUtility.DisplayDialog("Prefab References", "No referenced prefabs found.", "OK");
        }
    }
}


