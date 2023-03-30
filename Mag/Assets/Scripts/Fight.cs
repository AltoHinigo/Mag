using System;
using System.Timers;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using static UnityEditor.Progress;
//using Unity.VisualScripting;
//using System.Numerics;

public class Fight : MonoBehaviour
{
    public Vector3 BeginVec3 = new Vector3(0,1,0);

    public Vector3 EndVec3 = new Vector3(0, 1, 4);

    public int switch_on = 0;

    private static List<GameObject> UnderAttack = new List<GameObject>();

    private static Timer aTimer;

    private static Timer bTimer;

    private int[] Elements = new int[5];

    public GameObject ParticleSystemObject;

    private ParticleSystem aParticleSystem;

    private bool TimeIsOver = false;

    private bool TimeAttackTic = false;

    [SerializeField] GameObject PlayerStats;

    private Stats _Stats;

    private MortalObject _MortalObject;

    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerStats.TryGetComponent<Stats>(out _Stats))
            Debug.Log("ERROR TryGetComponent<Stats>");
        aParticleSystem = ParticleSystemObject.GetComponent<ParticleSystem>();
        aTimer = new Timer(5000);
        aTimer.Elapsed += OnTimer;
        aTimer.AutoReset = false;

        bTimer = new Timer(200);
        bTimer.Elapsed += OnbTimer;
    }
    private void OnTimer(object s, ElapsedEventArgs e)
    {
        TimeIsOver = true;
    }

    private void OnbTimer(object s, ElapsedEventArgs e)
    {
        TimeAttackTic = true;
    }

    void Update()
    {
        if (TimeAttackTic)
        {
            switch (switch_on)
            {
                case 0:
                    fire();
                    break;
                case 1:
                    lazer();
                    break;
                //default:
            }
            for (int i = 0; i < UnderAttack.Count; i++)
            {
                //_Stats.ChangeHP(-1);
                if (UnderAttack[i].TryGetComponent<MortalObject>(out _MortalObject))
                {
                    _MortalObject.ChangeHP(-1);
                }
                /*if (UnderAttack[i].gameObject.GetComponent<Renderer>().material.GetColor("_Color") == Color.red)
                    UnderAttack[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                else
                    UnderAttack[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);*/
            }
            TimeAttackTic = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            aTimer.Start();
            bTimer.Start();
            aParticleSystem.Play();
        }
        if (TimeIsOver)
        {
            foreach (ref int element in Elements.AsSpan())
                element = 0;
            aParticleSystem.Pause();
            aParticleSystem.Clear();
            TimeIsOver = false;
            bTimer.Stop();
        }
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
                //Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(x, y, 0)), transform.TransformDirection(new Vector3(x, y, dist)));
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
