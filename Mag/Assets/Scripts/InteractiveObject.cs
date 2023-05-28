using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public float Dist;
    public Action<GameObject> OnEnter;
    public Action<GameObject> OnInside;
    public Action<GameObject> OnLeave;
    public Action<GameObject> OnUse;
    public Action Run;
    private void Start()
    {
        if(gameObject.layer != 9)
            gameObject.layer = 9;
    }
    //public abstract void Script(GameObject Self);

    public virtual void Use(GameObject Self)
    { return; }
}
