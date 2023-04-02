using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectInfo : MonoBehaviour
{
    [SerializeField] private string _Caption = "";
    [SerializeField] private int _Damage = 0;
    [SerializeField] private int _Time = 0;
    [SerializeField] private float _SpeedEffect = 0;
    [SerializeField] private int _MaxHPEffect = 0;

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
