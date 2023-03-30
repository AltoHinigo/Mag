using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MortalObject : MonoBehaviour
{
    [SerializeField] private int _MaxHP = 10;
    [SerializeField] private int _HP = 10;
    [SerializeField] private int _MagicStaffNow = 2;
    [SerializeField] private GameObject[] _MagicStaff;
    [Header("Health Bar Settings")]
    [SerializeField] private GameObject _Canvas;
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
        if(_HP == _MaxHP && _Canvas.gameObject.activeSelf)
            _Canvas.gameObject.SetActive(false);
        else if (_HP != _MaxHP && !_Canvas.gameObject.activeSelf)
            _Canvas.gameObject.SetActive(true);
        _HP += HP;
        if (_HP > 0 && _HP < int.MaxValue)
        {
            _BarFiller.fillAmount = (_HP / (_MaxHP * 1.0f));
        }
        else
            this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _Canvas.gameObject.SetActive(false);

        //_ChangeBar = GetComponent<ChangeBar>();
        _BarFiller.fillAmount = (_HP / (_MaxHP * 1.0f));
        //_BarFiller.color = _BarColorFiller;
        //_Bar.color = _BarColor;
        /*for (int i = 0; i < _MagicStaff.Length; i++)
        {
            _MagicStaff[i].SetActive(false);
        }
        if (_MagicStaffNow > -1 && _MagicStaffNow < _MagicStaff.Length)
            _MagicStaff[_MagicStaffNow].SetActive(true);
        else
            _MagicStaff[0].SetActive(true);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
