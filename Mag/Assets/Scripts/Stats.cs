using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int _MaxHP = 10;
    [SerializeField] private int _HP = 10;
    [SerializeField] private int _Speed = 10;
    [SerializeField] private int _MagicStaffNow = 2;
    [SerializeField] private GameObject[] _MagicStaff;
    private int _MaxHPEffect = 0;
    private int _SpeedEffect = 0;
    [Header("Health Bar Settings")]
    [SerializeField] private GameObject _Bar;
    private StatusBar HPBar;


    public void ChangeMagicStaff(int MagicStaff)
    {
        if (MagicStaff > -1 && MagicStaff < _MagicStaff.Length)
        {
            _MagicStaff[MagicStaff].SetActive(true);
            _MagicStaffNow = MagicStaff;
        }
    }

    public void ChangeHP(int HP)
    {
        //Debug.Log("HP"+ _HP);
        _HP += HP;
        if (_HP > (_MaxHP+_MaxHPEffect))
        {
            _HP = _MaxHP;
            return;
        }
        if (_HP > 0)
            HPBar.ChangeFill(_HP / (_MaxHP * 1.0f));
        else
        {
            Debug.Log("GameOver");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        HPBar = _Bar.GetComponent<StatusBar>();
        HPBar.ChangeFill(_HP / (_MaxHP * 1.0f));
        //_ChangeBar = GetComponent<ChangeBar>();
        //_BarFiller.color = _BarColorFiller;
        //_Bar.color = _BarColor;
        for (int i = 0; i < _MagicStaff.Length; i++)
        {
            _MagicStaff[i].SetActive(false);
        }
        if(_MagicStaffNow > -1 && _MagicStaffNow < _MagicStaff.Length)
            _MagicStaff[_MagicStaffNow].SetActive(true);
        else
            _MagicStaff[0].SetActive(true);
    }

    private void Start()
    {
        
        
    }

    

    
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
