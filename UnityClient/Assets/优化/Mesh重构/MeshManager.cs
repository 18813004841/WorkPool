using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    string[] CombineTextureNameList = new string[1] { "_BaseMap" };
    //合并后的贴图数组
    RenderTexture[] combineTextures;

    private void OnEnable()
    {
        CombineMesh();
    }

    private void CombineMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Material[] materials = new Material[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            materials[i] = meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial;
            meshFilters[i].gameObject.SetActive(false);
        }
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshFilter>().mesh = new Mesh();
        gameObject.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        gameObject.GetComponent<MeshRenderer>().materials = materials;
        gameObject.SetActive(true);

    }
}
