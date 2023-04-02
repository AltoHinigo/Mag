using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectInfo : MonoBehaviour
{
    [SerializeField] private int _Damage = 0;
    [SerializeField] private int _Time = 0;
    [SerializeField] private int _Speed = 0;

    public Effect Info;
    // Start is called before the first frame update
    void Start()
    {
        Info = new Effect(-_Damage, _Time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
