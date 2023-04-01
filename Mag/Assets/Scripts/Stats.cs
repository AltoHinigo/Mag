using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
public class CEffect
{
    public bool TimeOver = false;
    public string Caption = "";
    public double EffectTime = 1;
    public int TikDamage = 0;
    public int EffectSpeed = 0;
    public int EffectMaxHP = 0;
    public Timer timer;

    public CEffect(string caption, double effectTime,
        int tikDamage, int effectSpeed, int effectMaxHP)
    {
        Caption = caption;
        EffectTime = effectTime;
        TikDamage = tikDamage;
        EffectSpeed = effectSpeed;
        EffectMaxHP = effectMaxHP;
        EffectTime = effectTime;
        timer = new Timer(effectTime);
        timer.AutoReset = false;
        timer.Elapsed += (sender, e) =>
        {
            TimeOver = true;
        };
        timer.Start();
    }
}

public class Stats : MonoBehaviour
{
    [SerializeField] private int _MaxHP = 10;
    [SerializeField] private int _HP = 10;
    [SerializeField] private int _Speed = 10;
    [SerializeField] private int _MagicStaffNow = 2;
    [SerializeField] private GameObject[] _MagicStaff;
    private List<CEffect> Effects;
    private int _MaxHPEffect = 0;
    private int _SpeedEffect = 0;
    private Timer _TimerTick;
    private int Tick = 200;
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
        Debug.Log("HP" + _HP);
        if (_HP > (_MaxHP+_MaxHPEffect))
        {
            _HP = _MaxHP;
            return;
        }
        Debug.Log("(_HP > 0)" + (_HP > 0));
        if (_HP > 0)
        {
            _BarFiller.fillAmount = (_HP / (_MaxHP * 1.0f));
            Debug.Log("Work!");
        }
        else
        {
            Debug.Log("GameOver");
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

    private void Start()
    {
        _TimerTick = new Timer(Tick);
        _TimerTick.AutoReset = true;
        _TimerTick.Elapsed += _TimerTick_Elapsed;
        _TimerTick.Start();
        Effects = new List<CEffect>();
    }

    private void _TimerTick_Elapsed(object sender, ElapsedEventArgs e)
    {
        int i = 0;
        while (i < Effects.Count)
        {
            if (Effects[i].TimeOver && false)
            {
                i++;
            }//Effects.RemoveAt(i);
            else
            {
                Debug.Log("i++" + i);
                
                Debug.Log("! " + Effects[i].TikDamage);
                Debug.Log(Effects[i].TikDamage != 0);
                if (Effects[i].TikDamage != 0)
                {
                    Debug.Log("ChangeHP " + Effects[i].TikDamage);
                    ChangeHP(Effects[i].TikDamage);
                }
                i++;
                //if (TMP.EffectSpeed != 0)
                //ChangeSpeed();
            }
        }
        //throw new NotImplementedException();
    }

    private void OnApplicationQuit()
    {
        _TimerTick.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEffect(double EffectTime, int TikDamage = 0, int EffectSpeed = 0,
        int EffectMaxHP = 0, string Caption = "")
    {
        for (int i = 0; i < Effects.Count; i++)
            if (Effects[i].Caption == Caption)
                return;
        Debug.Log("EffectTime " + EffectTime);
        Effects.Add(new CEffect(Caption, EffectTime, TikDamage, EffectSpeed, EffectMaxHP));
    }
}
