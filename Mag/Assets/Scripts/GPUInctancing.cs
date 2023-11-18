using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInctancing : MonoBehaviour
{
    void Start()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

}
