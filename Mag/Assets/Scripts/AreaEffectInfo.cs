using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectInfo : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        //Info = new Effect(-_Damage, _Time);
        Info = new Effect(_Caption, _Time, -_Damage, _SpeedEffect, _MaxHPEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
