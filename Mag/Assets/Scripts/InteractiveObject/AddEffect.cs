using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEffect : InteractiveObject
{
    [Header("ID effect")]
    [SerializeField] private string _Caption = "";

    [Header("Status effects")]
    [SerializeField] private int _Damage = 0;
    [SerializeField] private int _Time = 0;
    [SerializeField] private float _SpeedEffect = 0;
    [SerializeField] private int _MaxHPEffect = 0;

    [Header("Elemental effects")]
    [SerializeField] private bool _WaterTimeless = false;
    [SerializeField] private int _WaterTime = 0;
    [SerializeField] private bool _FireTimeless = false;
    [SerializeField] private int _FireTime = 0;
    [SerializeField] private bool _ElectrolyzedTimeless = false;
    [SerializeField] private int _ElectrolyzedTime = 0;
    public Effect Info;

    void Start()
    {
        Info = new Effect(_Caption, _Time, -_Damage, _SpeedEffect, _MaxHPEffect);
        OnInside += _AddEffect;
    }

    /*public void Script(GameObject Self)
    {
        _AddEffect(Self);
    }*/

    private void _AddEffect(GameObject gameObject)
    {
        if(gameObject.TryGetComponent(out Effects effects))
            effects.AddEffect(Info);
    }
}
