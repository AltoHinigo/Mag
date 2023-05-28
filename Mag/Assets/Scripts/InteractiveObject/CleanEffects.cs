using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanEffects : InteractiveObject
{
    private void Start()
    {
        OnEnter += ClearEffects;
    }
    /*public override void Script(GameObject Self)
    {
        ClearEffects(Self);
    }*/
    private void ClearEffects(GameObject gameObject)
    {
        gameObject.TryGetComponent(out Effects effects);
        effects.DelAllEffects();
    }
}
