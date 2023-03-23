using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class FindPrefabReferences : EditorWindow
{
    private static Object _selectedPrefab;
    public static List<string> _referencedPrefabs = new List<string>();
    public static bool _isSearching = false;
    private static string[] guids;

    [MenuItem("Assets/Find Prefab References", true)]
    private static bool ValidateFindPrefabReferences()
    {
        return Selection.activeObject != null && Selection.activeObject is GameObject;
    }

    [MenuItem("Assets/Find Prefab References")]
    private static void FindPrefabReferencesFunc()
    {
        _selectedPrefab = Selection.activeObject;
        _referencedPrefabs.Clear();
        _isSearching = true;

        guids = AssetDatabase.FindAssets("t:Prefab");

        EditorApplication.update += SearchPrefabReferences;

        PrefabReferencesWindow window = GetWindow<PrefabReferencesWindow>("Prefab References");
        window.Show();
    }

    private static void SearchPrefabReferences()
    {
        if (!_isSearching)
        {
            EditorApplication.update -= SearchPrefabReferences;
            return;
        }

        if (_referencedPrefabs.Count >= 1000)
        {
            _isSearching = false;
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Prefab References", "Too many references found, try a more specific search.", "OK");
            return;
        }

        float progress = 0f;

        if (_referencedPrefabs.Count > 0)
        {
            progress = (float)(_referencedPrefabs.Count - 1) / 100f;
        }

        EditorUtility.DisplayProgressBar("Find Prefab References", "Finding referenced prefabs...", progress);

        string prefabPath = AssetDatabase.GUIDToAssetPath(guids[_referencedPrefabs.Count]);
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

        progress = (float)_referencedPrefabs.Count / 100f;
        EditorUtility.DisplayProgressBar("Find Prefab References", "Finding referenced prefabs...", progress);

        if (_referencedPrefabs.Count == guids.Length)
        {
            _isSearching = false;
            EditorUtility.ClearProgressBar();
        }
    }

    public static void StopSearch()
    {
        _isSearching = false;
        EditorUtility.ClearProgressBar();
        EditorApplication.update -= SearchPrefabReferences;
    }
}
