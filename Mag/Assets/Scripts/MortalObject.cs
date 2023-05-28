using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MortalObject : MonoBehaviour
{
    [SerializeField] private int _MaxHPDefault= 10;
    [SerializeField] private int _HPEffect = 10;
    [SerializeField] private int _HPNow = 10;
    [SerializeField] private float _Speed = 3.5f;
    [SerializeField] private float _SpeedEffect = 0f;
    //[SerializeField] private int _MagicStaffNow = 2;
    //[SerializeField] private GameObject[] _MagicStaff;
    [Header("Health Bar Settings")]
    [SerializeField] private GameObject _Canvas;
    [SerializeField] private Image _Bar;
    [SerializeField] private Color _BarColor;
    [SerializeField] private Image _BarFiller;
    [SerializeField] private Color _BarColorFiller;
    private float _BarFill;
    private NavMeshAgent _NavMeshAgent;
    public void ChangeHP(int HP)
    {
        if (_HPNow != _MaxHPDefault&& !_Canvas.gameObject.activeSelf)
            _Canvas.gameObject.SetActive(true);
        _HPNow += HP;
        if(_HPNow >= (_HPEffect + _MaxHPDefault))
        {
            _HPNow = (_HPEffect + _MaxHPDefault);
            _Canvas.gameObject.SetActive(false);
        }
        if (_HPNow > 0 && _HPNow < int.MaxValue)
        {
            _BarFiller.fillAmount = (_HPNow / (_MaxHPDefault* 1.0f));
        }
        else
            this.gameObject.SetActive(false);
    }

    public void ChangeSpeed(float SpeedEffect)
    {
        //Debug.Log("HP"+ _HP);_SpeedEffect
        if ((_Speed + _SpeedEffect + SpeedEffect) < 0)
        {
            _SpeedEffect = -_Speed;
            _NavMeshAgent.speed = 0;
            return;
        }
        _SpeedEffect += SpeedEffect;
        _NavMeshAgent.speed = _Speed + _SpeedEffect;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _Canvas.gameObject.SetActive(false);
        _BarFiller.fillAmount = (_HPNow / (_MaxHPDefault* 1.0f));
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
