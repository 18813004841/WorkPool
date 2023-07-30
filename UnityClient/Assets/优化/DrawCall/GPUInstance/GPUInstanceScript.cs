using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInstanceScript : MonoBehaviour
{
    public Texture texture;
    MeshRenderer _meshRender;
    public int type;
    private void Start()
    {
        _meshRender = GetComponent<MeshRenderer>();
        MaterialPropertyBlock prop = new MaterialPropertyBlock();
        prop.SetColor("Color2", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        type = Random.Range(1, 4);
        prop.SetInt("Type", type);
        _meshRender.SetPropertyBlock(prop);
    }
}
