using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
//using Unity.VisualScripting;
//using Unity.VisualScripting;
//using System.Numerics;

public class Fight : MonoBehaviour
{
    public Vector3 BeginVec3 = new Vector3(0,1,0);

    public Vector3 EndVec3 = new Vector3(0, 1, 4);

    public int switch_on = 0;

    private static List<GameObject> UnderAttack = new List<GameObject>();

    private static Timer TimeAttack;

    private static Timer TimeAttackTic;

    private int[] Elements = new int[5];

    public ParticleSystemChange aParticleSystem;

    public ParticleSystemChange aParticleSystemSelf;

    public ParticleSystemChange aParticleSystemAoE;

    //public GameObject ParticleSystemObject;

    //private ParticleSystem aParticleSystem;

    private bool TimeIsOver = false;

    private bool AttackTic = false;

    [SerializeField] GameObject PlayerStats;

    [SerializeField] Text _Elements;

    private Stats _Stats;

    private MortalObject _MortalObject;

    private bool Attack = false;

    private bool Use = false;

    private bool UseSelf = false;

    private bool AoE = false;

    private int MagicTime = 0;

    private int Fire = 0;
    private int Water = 0;
    private int Life = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerStats.TryGetComponent<Stats>(out _Stats))
            Debug.Log("ERROR TryGetComponent<Stats>");
        //aParticleSystem = ParticleSystemObject.GetComponent<ParticleSystem>();
        TimeAttack = new Timer(5000);
        TimeAttack.Elapsed += OnAttackTime;
        TimeAttack.AutoReset = false;

        TimeAttackTic = new Timer(200);
        TimeAttackTic.Elapsed += OnTimeAttackTic;
    }
    private void OnAttackTime(object s, ElapsedEventArgs e)
    {
        TimeIsOver = true;
    }

    private void OnTimeAttackTic(object s, ElapsedEventArgs e)
    {
        AttackTic = true;
    }

    void Update()
    {
        if (AttackTic)
        {
            if (AoE)
            {
                AttackTic = false;
                return;
            }
            if (UseSelf)
            {
                _Stats.ChangeHP(Life * 2 - Fire * 2);
                /*if(Fire == 0 && Life > 0)
                    _Stats.ChangeHP(Life*5);*/
                AttackTic = false;
                return;
            }
            if (Use)
            {
                
                switch (_Stats.MagicStaffNow)
                {
                    case 0:
                        fire();
                        break;
                    case 1:
                        lazer();
                        break;
                    case 2:
                        lazer();
                        break;
                        //default:
                }
                for (int i = 0; i < UnderAttack.Count; i++)
                {
                    //_Stats.ChangeHP(-1);
                    if (UnderAttack[i].TryGetComponent<MortalObject>(out _MortalObject))
                    {
                        _MortalObject.ChangeHP(Life * 2 - Fire * 2);
                    }
                    /*if (UnderAttack[i].gameObject.GetComponent<Renderer>().material.GetColor("_Color") == Color.red)
                        UnderAttack[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    else
                        UnderAttack[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);*/
                }
                AttackTic = false;
            }
        }

        if (Input.GetKey(KeyCode.W) || Attack)
        {
            Fire = 0;
            Water = 0;
            Life = 0;
            foreach (var element in Elements)
                switch (element)
                {
                    case 1:
                        Fire++;
                        break;
                    case 2:
                        Water++;
                        break;
                    case 3:
                        Life++;
                        break;
                }
            Attack = false;
            MagicTime = 500;
            foreach (var element in Elements)
                if (element != 0)
                    MagicTime += 500;
            if (MagicTime > 500)
            {
                TimeAttack.Interval = MagicTime;
                TimeAttack.Start();
                TimeAttackTic.Start();
                if (Use)
                {
                    aParticleSystem.SetPrefab(new Color(1.0f * Fire, 1.0f * Life, 1.0f * Water), new Color(1.0f, 1.0f, 1.0f));
                    aParticleSystem.Play();
                }
                if (UseSelf)
                {
                    aParticleSystemSelf.SetPrefab(new Color(1.0f * Fire, 1.0f * Life, 1.0f * Water), new Color(1.0f, 1.0f, 1.0f));
                    aParticleSystemSelf.Play();
                }
                if (AoE)
                {
                    aParticleSystemAoE.SetPrefab(new Color(1.0f * Fire, 1.0f * Life, 1.0f * Water), new Color(1.0f, 1.0f, 1.0f));
                    aParticleSystemAoE.Play();
                }
            }
            else
            {
                Use = false;
                UseSelf = false;
                AoE = false;
            }
        }
        if (TimeIsOver)
            OnTimeIsOver();
    }

    private void OnTimeIsOver()
    {
        foreach (ref int element in Elements.AsSpan())
                element = 0;
        ShowElements();
        if(Use)
        {
            aParticleSystem.Pause();
            aParticleSystem.Clear();
        }
        if(UseSelf)
        {
            aParticleSystemSelf.Pause();
            aParticleSystemSelf.Clear();
        }
        if (AoE)
        {
            aParticleSystemAoE.Pause();
            aParticleSystemAoE.Clear();
        }
        Use =false;
        UseSelf = false;
        AoE = false;
        TimeIsOver = false;
        TimeAttackTic.Stop();
    }

        private void ShowElements()
    {
        string str = "";
        foreach (var element in Elements)
        {

            switch (element)
            {
                case 0:
                    str += " _";
                    break;
                case 1:
                    str += "F_";
                    break;
                case 2:
                    str += "W_";
                    break;
                case 3:
                    str += "L_";
                    break;
                default:
                    str += "?_";
                    break;
            }
            _Elements.text = str;
        }
    }

    public void ButtonOnClickFire()
    {
        foreach (ref int element in Elements.AsSpan())
            if (element == 0)
            {
                element = 1;
                ShowElements();
                return;
            }
    }

    public void ButtonOnClickWater()
    {
        foreach (ref int element in Elements.AsSpan())
            if (element == 0)
            {
                element = 2;
                ShowElements();
                return;
            }
    }

    public void ButtonOnClickLife()
    {
        foreach (ref int element in Elements.AsSpan())
            if (element == 0)
            {
                element = 3;
                ShowElements();
                return;
            }
    }

    public void ButtonOnClickChange()
    {
        _Stats.MagicStaffNow = _Stats.MagicStaffNow + 1;
    }

    public void ButtonOnClickUse()
    {
        Attack = true;
        Use = true;
    }

    public void ButtonOnClickUseSelf()
    {
        Attack = true;
        UseSelf = true;
    }

    public void ButtonOnClickAoE()
    {
        Attack = true;
        AoE = true;
    }

    void fire()
    {
        LayerMask layerMask = 1 << 8;
        float dist = 4.0f;
        bool flag = false;
        RaycastHit[] hits;
        int hit = 0;
        UnderAttack.Clear();
        for (float y = -1; y != 2; y += 0.5f)
            for (float x = -1; x != 2; x += 0.5f)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(x, y, dist)), Color.yellow);
                hits = Physics.RaycastAll(transform.position, transform.TransformDirection(new Vector3(x, y, dist)), dist, layerMask);
                if (hits.Length != 0)
                {
                    while(hit < hits.Length)
                    {
                        for (int MortalObject = 0; MortalObject < UnderAttack.Count; MortalObject++)
                        {
                            if (UnderAttack[MortalObject].GetHashCode() == hits[hit].collider.gameObject.GetHashCode())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if(!flag)
                        {
                            UnderAttack.Add(hits[hit].collider.gameObject);
                        }
                        else
                            flag = false;
                        hit++;
                    }
                }
            }
    }

    void lazer()
    {
        LayerMask layerMask = 1 << 8;
        float dist = 30.0f;
        bool flag = false;
        RaycastHit[] hits;
        int hit = 0;
        UnderAttack.Clear();
        for (float y = -0.2f; y != 0.2f; y += 0.1f)
            for (float x = -0.2f; x != 0.2f; x += 0.1f)
            {
                Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(x, y, 0)), transform.TransformDirection(new Vector3(x, y, dist)), Color.red);
                hits = Physics.RaycastAll(transform.position + transform.TransformDirection(new Vector3(x, y, 0)), transform.TransformDirection(new Vector3(x, y, dist)), dist, layerMask);
                if (hits.Length != 0)
                {
                    while (hit < hits.Length)
                    {
                        for (int MortalObject = 0; MortalObject < UnderAttack.Count; MortalObject++)
                        {
                            if (UnderAttack[MortalObject].GetHashCode() == hits[hit].collider.gameObject.GetHashCode())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            UnderAttack.Add(hits[hit].collider.gameObject);
                        }
                        else
                            flag = false;
                        hit++;
                    }
                }
            }
    }

    void fire1()
    {
        bool flag = false;
        RaycastHit hit;
        /*Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*4.0f, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4.0f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Get one");
        }
        else
        {
            Debug.Log("Miss");
        }*/
        UnderAttack.Clear();
        for (float y = -1; y != 2; y += 0.5f)
            for(float x = -1; x != 2; x += 0.5f)
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(x, y, 4)), Color.red);
                if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(x,y,4)), out hit, 4.0f, 1 << 8))
                {
                    //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(x, y, 4)) * hit.distance, Color.green);
                    //Debug.DrawRay(transform.position, hit.transform.TransformDirection(new Vector3(x, y, 4)), Color.green);
                    if(UnderAttack.Count == 0)
                        UnderAttack.Add(hit.collider.gameObject);
                    else
                        for (int i = 0; i < UnderAttack.Count; i++)
                        {
                            if (hit.collider.gameObject.GetHashCode() != UnderAttack[i].GetHashCode())
                                flag = true;
                        }
                    if (flag)
                    {
                        UnderAttack.Add(hit.collider.gameObject);
                        flag = false;
                    }
                }
            }
    }
}
