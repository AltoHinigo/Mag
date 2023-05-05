using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private Animator _Animator;
    [SerializeField] private float _DistAttack;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
    private List<GameObject> UnderAttack = new List<GameObject>();
    public bool HearTarget = false;
    private Effects _PlrEffects;
    [SerializeField] private int _AttackDamage = 0;
    [SerializeField] private int TickTime = 5000;
    [Header("ID effect")]
    [SerializeField] private string _Caption = "";

    [Header("Status effects")]
    [SerializeField] private int _Damage = 0;
    [SerializeField] private int _Time = 0;
    [SerializeField] private float _SpeedEffect = 0;
    [SerializeField] private int _MaxHPEffect = 0;

    [Header("Elemental effects")]
    [SerializeField] private bool _WaterTimeless = false;
    [SerializeField] private int _WaterTime = 0;
    [SerializeField] private bool _FireTimeless = false;
    [SerializeField] private int _FireTime = 0;
    [SerializeField] private bool _ElectrolyzedTimeless = false;
    [SerializeField] private int _ElectrolyzedTime = 0;

    public Effect Info;
    private Stats _PlayerStst;
    private bool Tick = true;
    private bool Attack = false;
    private bool PlayerIsDied = false;
    private Timer TimeAttackTic;

    private List<GameObject> UnderEffects = new List<GameObject>();

    void Start()
    {
        GlobalEventManager.OnDeath += killed;

        TimeAttackTic = new Timer(TickTime);
        TimeAttackTic.Elapsed += OnTimeAttackTic;

        m_Agent = GetComponent<NavMeshAgent>();
        _PlrEffects = _Player.GetComponent<Effects>();
        Info = new Effect(this.name, _Time, -_Damage, _SpeedEffect, _MaxHPEffect);
        _PlayerStst = _Player.GetComponent<Stats>();
    }

    public void killed()
    {
        Attack = false;
        Tick = false;
        PlayerIsDied = true;
        m_Agent.CompleteOffMeshLink();
    }

    private void OnTimeAttackTic(object s, ElapsedEventArgs e)
    {
        Attack = true;
        Tick = true;
        HearTarget = false;
        m_Agent.CompleteOffMeshLink();
    }
    void LateUpdate()
    {
        if (Attack)
        {
            Debug.Log(this.name + " _AttackDamage " + _AttackDamage);
            //_PlrEffects.AddEffect(new Effect(this.name+"_Damage", 1, -_AttackDamage, 0, 0));
            _PlayerStst.ChangeHP(-_AttackDamage);
            Attack = false;
        }
        float dist = Vector3.Distance(_Player.transform.position, transform.position);
        //Debug.Log("Dist " + dist);
        if (dist < 1.5f)
        {
            Vector3 focus = Vector3.Scale(_Player.transform.position, new Vector3(1, 0, 1));
            focus.y = transform.position.y;
            transform.LookAt(focus);

            _Animator.SetBool("Walk", false);
            _Animator.SetBool("Attack", true);
            m_Agent.CompleteOffMeshLink();
            if (Tick)
            {
                Tick = false;
                _PlrEffects.AddEffect(Info);
                TimeAttackTic.Start();
            }
            return;
        }
        else
        {
            Tick = true;
            TimeAttackTic.Stop();
            _Animator.SetBool("Attack", false);
        }
        if ((SeesTarget() || HearTarget) && !PlayerIsDied)
        {
            HearTarget = false;
            m_Agent.destination = _Player.transform.position - transform.forward;
            Alarm();
        }
        else
        if (PlayerIsDied)
            PlayerIsDied = false;


        if (m_Agent.hasPath)
        {
            _Animator.SetBool("Walk", true);
        }
        else
        {
            _Animator.SetBool("Walk", false);
        }
        
    }

    private void Alarm()
    {
        GetAreas();
        for (int i = 0; i < UnderEffects.Count; i++)
        {
            AIHear _AIHear;
            if (UnderEffects[i].TryGetComponent<AIHear>(out _AIHear))
                _AIHear.HearTarget(true);
        }
    }

    void GetAreas()
    {
        LayerMask layerMask = 1 << 9;
        float dist = 30.0f;
        bool flag = false;
        RaycastHit[] hits;
        int hit = 0;
        UnderEffects.Clear();
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0, -1, 0)), transform.TransformDirection(new Vector3(0, 3, 0)), Color.red);
        hits = Physics.RaycastAll(transform.position + transform.TransformDirection(new Vector3(0, -1, 0)), transform.TransformDirection(new Vector3(0, 3, 0)), dist, layerMask);
        if (hits.Length != 0)
        {
            while (hit < hits.Length)
            {
                for (int MortalObject = 0; MortalObject < UnderEffects.Count; MortalObject++)
                {
                    if (UnderEffects[MortalObject].GetHashCode() == hits[hit].collider.gameObject.GetHashCode())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    UnderEffects.Add(hits[hit].collider.gameObject);
                }
                else
                    flag = false;
                hit++;
            }
        }
    }

    /*private async void AttackTick()
    {
        await System.Threading.Tasks.Task.Delay(TickTime);
        _PlayerStst.ChangeHP(-_AttackDamage);
        Tick = true;
        Attack = false;
    }*/

    private bool SeesTarget()
    {
        LayerMask layerMask = 1 << 10;
        float dist = 8.0f;
        bool flag = false;
        RaycastHit[] hits;
        int hit = 0;
        UnderAttack.Clear();
        for (float y = -0.5f; y != 1; y += 0.5f)
            for (float x = -3; x != 4; x += 0.5f)
            {
                Debug.DrawRay(transform.position - transform.forward, transform.TransformDirection(new Vector3(x, y, dist)), Color.yellow);
                hits = Physics.RaycastAll(transform.position - transform.forward, transform.TransformDirection(new Vector3(x, y, dist)), dist, layerMask);
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
        return UnderAttack.Count != 0;
    }
}
