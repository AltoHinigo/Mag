using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int _MaxHPDefault = 10;
    [SerializeField] private int _HPEffect = 0;
    [SerializeField] private int _HPNow = 10;
    [SerializeField] private float _Speed = 10;
    [SerializeField] private float _SpeedEffect = 0;
    [SerializeField] private int _MagicStaffNow = 2;
    [SerializeField] private GameObject[] _MagicStaff;
    [SerializeField] private JoyStickMovement _JoyStickMovement;
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
        //Debug.Log("HP" + _HPNow);
        if (HP < 0 && _HPNow < -HP)
        {
            _HPNow = 0;
            HPBar.ChangeFill(0);
            Debug.Log("GameOver");
            return;
        }
        _HPNow += HP;
        if (_HPNow > (_MaxHPDefault + _HPEffect))
            _HPNow = _MaxHPDefault + _HPEffect;
        HPBar.ChangeFill(_HPNow / ((_MaxHPDefault + _HPEffect) * 1.0f));
    }

    public void ChangeHPMax(int HPEffect)
    {
        //Debug.Log("HP"+ _HP);
        if (!((_HPEffect + _MaxHPDefault + HPEffect) > 0))
        {
            _HPNow = 1;
            HPBar.ChangeFill(1);
            return;
        }
        if (_HPNow > (_HPEffect + _MaxHPDefault + HPEffect))
            _HPNow = _HPEffect + _MaxHPDefault + HPEffect;
        _HPEffect += HPEffect;
        HPBar.ChangeFill(_HPNow / ((_MaxHPDefault + _HPEffect) * 1.0f));
    }

    public void ChangeSpeed(float SpeedEffect)
    {
        //Debug.Log("HP"+ _HP);_SpeedEffect
        if ((_Speed + _SpeedEffect + SpeedEffect) < 0)
        {
            _Speed = 0;
            _JoyStickMovement.ChangeSpeed(0);
            return;
        }
        _SpeedEffect += SpeedEffect;
        _JoyStickMovement.ChangeSpeed(_Speed + _SpeedEffect);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _JoyStickMovement = GetComponent<JoyStickMovement>();
        HPBar = _Bar.GetComponent<StatusBar>();
        HPBar.ChangeFill(_HPNow / ((_MaxHPDefault + _HPEffect) * 1.0f));

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
