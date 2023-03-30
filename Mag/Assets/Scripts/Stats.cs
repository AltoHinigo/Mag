using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private int _MaxHP = 10;
    [SerializeField] private int _HP = 10;
    [SerializeField] private int _MagicStaffNow = 2;
    [SerializeField] private GameObject[] _MagicStaff;
    [Header("Health Bar Settings")]
    [SerializeField] private Image _Bar;
    [SerializeField] private Color _BarColor;
    [SerializeField] private Image _BarFiller;
    [SerializeField] private Color _BarColorFiller;
    private float _BarFill;
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
        _HP += HP;
        if (_HP > 0 && _HP < int.MaxValue)
        {
            //Debug.LogFormat("_HP {0}; HP {1}; _HP / _MaxHP {2}", _HP, HP, (_HP / (_MaxHP * 1.0f)));
            _BarFiller.fillAmount = (_HP / (_MaxHP * 1.0f));
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //_ChangeBar = GetComponent<ChangeBar>();
        _BarFiller.fillAmount = (_HP / (_MaxHP * 1.0f));
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
