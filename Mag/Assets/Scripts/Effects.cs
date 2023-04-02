using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Effect
{
    public bool TimeOver = false;
    public string Caption = "";
    public double EffectTime = 1;
    public int TikDamage = 0;
    public int EffectSpeed = 0;
    public int EffectMaxHP = 0;
    public Timer timer;

    public Effect(int tikDamage, int effectTime)
    {
        TikDamage = tikDamage;
        EffectTime = effectTime;
        timer = new Timer(effectTime);
        timer.AutoReset = false;
        timer.Elapsed += (sender, e) =>
        {
            TimeOver = true;
        };
    }
    public Effect(string caption, double effectTime,
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

    public void TimerStop()
    {
        timer.Stop();
    }

    public void TimerStart()
    {
        timer.Start();
    }
}


public class Effects : MonoBehaviour
{
    [SerializeField] private Stats Stat;
    private List<Effect> _Effects;
    private int Tick = 200;
    private Timer _TimerTick;

    private bool Flags = false;

    // Start is called before the first frame update
    void Start()
    {
        _TimerTick = new Timer(Tick);
        _TimerTick.AutoReset = true;
        _TimerTick.Elapsed += _TimerTick_Elapsed;
        _TimerTick.Start();
        _Effects = new List<Effect>();

    }

    private void _TimerTick_Elapsed(object sender, ElapsedEventArgs e)
    {

        Flags = true;
        //throw new NotImplementedException();
    }

    private void OnApplicationQuit()
    {
        _TimerTick.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Flags) return;
        Flags = false;
        int i = 0;
        while (i < _Effects.Count)
        {
            if (_Effects[i].TimeOver)
            {
                _Effects.RemoveAt(i);
            }
            else
            {
                if (_Effects[i].TikDamage != 0)
                    Stat.ChangeHP(_Effects[i].TikDamage);
                i++;
            }
        }
    }

    public void AddEffect(double EffectTime, int TikDamage = 0, int EffectSpeed = 0,
        int EffectMaxHP = 0, string Caption = "")
    {
        for (int i = 0; i < _Effects.Count; i++)
            if (_Effects[i].Caption == Caption)
                return;
        _Effects.Add(new Effect(Caption, EffectTime, TikDamage, EffectSpeed, EffectMaxHP));
    }
    public void AddEffect(Effect effect)
    {
        for (int i = 0; i < _Effects.Count; i++)
        if (_Effects[i].Caption == effect.Caption)
        {
            effect.TimeOver = false;
            effect.TimerStop();
            effect.TimerStart();
            return;
        }
        _Effects.Add(effect);
            
    }
}

