using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DEL : MonoBehaviour
{
    public Object parentEtalon;
    public Object parent;
    public MeshRenderer BB;
    // Start is called before the first frame update

    MeshRenderer A;
    void Start()
    {
        int i = 0;
        List<MeshRenderer> _childMeshRenderer = new List<MeshRenderer>();
        foreach (MeshRenderer childMeshRenderer in parentEtalon.GetComponentsInChildren<MeshRenderer>())
            _childMeshRenderer.Add(childMeshRenderer);
        foreach (MeshRenderer childMeshRenderer in parent.GetComponentsInChildren<MeshRenderer>())
        {
            childMeshRenderer.lightmapIndex = _childMeshRenderer[i].lightmapIndex;
            childMeshRenderer.lightmapScaleOffset = _childMeshRenderer[i].lightmapScaleOffset;
            i++;
        }
       /* mm.GetComponentInChildren<MeshRenderer>();
        A = GetComponent<MeshRenderer>();
        A.lightmapIndex = BB.lightmapIndex;
        A.lightmapScaleOffset = BB.lightmapScaleOffset;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
