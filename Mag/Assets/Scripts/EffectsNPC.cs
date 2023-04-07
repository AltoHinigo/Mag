using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

/*public class Effect
{
    public bool TimeOver = false;
    public string Caption = "";
    public double EffectTime = 1;
    public int TikDamage = 0;
    public float EffectSpeed = 0;
    public int EffectMaxHP = 0;
    public Timer timer;
    public bool EffectsUsed = false;

    public Effect(int tikDamage, int effectTime)
    {
        TikDamage = tikDamage;
        EffectTime = effectTime;
        if (effectTime == 0)
            timer = new Timer(1);
        else
            timer = new Timer(effectTime);
        timer.AutoReset = false;
        timer.Elapsed += (sender, e) =>
        {
            TimeOver = true;
        };
    }
    public Effect(string caption, int effectTime,
        int tikDamage, float effectSpeed, int effectMaxHP)
    {
        Caption = caption;
        EffectTime = effectTime;
        TikDamage = tikDamage;
        EffectSpeed = effectSpeed;
        EffectMaxHP = effectMaxHP;
        EffectTime = effectTime;
        if(effectTime == 0)
            timer = new Timer(1);
        else
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
}*/


public class EffectsNPC : MonoBehaviour
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
        string debugstr = "";//
        if(!Flags) return;
        Flags = false;
        int i = 0;
        while (i < _Effects.Count)
        {
            debugstr += _Effects[i].Caption + ";  ";//
            if (_Effects[i].TimeOver)
            {
                if (_Effects[i].EffectsUsed)
                {
                    if (_Effects[i].EffectSpeed != 0)
                        Stat.ChangeSpeed(-_Effects[i].EffectSpeed);
                    if (_Effects[i].EffectMaxHP != 0)
                        Stat.ChangeHPMax(-_Effects[i].EffectMaxHP);
                }
                _Effects.RemoveAt(i);
                //Debug.Log("! "+_Effects.Count);
            }
            else
            {
                if (_Effects[i].TikDamage != 0)
                    Stat.ChangeHP(_Effects[i].TikDamage);
                if (!_Effects[i].EffectsUsed)
                {
                    _Effects[i].EffectsUsed = true;
                    if (_Effects[i].EffectSpeed != 0)
                        Stat.ChangeSpeed(_Effects[i].EffectSpeed);
                    if (_Effects[i].EffectMaxHP != 0)
                        Stat.ChangeHPMax(_Effects[i].EffectMaxHP);
                }
                
                i++;
            }
        }
        Debug.Log(debugstr);//
    }

    public void AddEffect(int EffectTime, int TikDamage = 0, int EffectSpeed = 0,
        int EffectMaxHP = 0, string Caption = "")
    {
        for (int i = 0; i < _Effects.Count; i++)
            if (_Effects[i].Caption == Caption)
            {
                _Effects[i].EffectsUsed = false;
                return;
            }
        _Effects.Add(new Effect(Caption, EffectTime, TikDamage, EffectSpeed, EffectMaxHP));
    }
    public void AddEffect(Effect effect)
    {
        for (int i = 0; i < _Effects.Count; i++)
        if (_Effects[i].Caption == effect.Caption)
        {
            if(_Effects[i].EffectTime == 0)
            {
                return;
            } 
            else
            {
                effect.TimeOver = false;
                effect.TimerStop();
                effect.TimerStart();
            }
            return;
        }
        effect.TimerStop();
        effect.TimeOver = false;
        effect.EffectsUsed = false;
        _Effects.Add(effect);
            
    }
    public void DelEffect(string Caption)
    {
        for (int i = 0; i < _Effects.Count; i++)
            if (_Effects[i].Caption == Caption)
            {
                _Effects[i].TimeOver = true;
                return;
            }
    }

    public void DelAllEffects()
    {
        for (int i = 0; i < _Effects.Count; i++)
        {
            _Effects[i].TimeOver = true;
        }
    }
}

