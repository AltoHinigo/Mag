using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
    private static List<GameObject> UnderAttack = new List<GameObject>();

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (SeesTarget())
            m_Agent.destination = _Player.transform.position - transform.forward;
    }
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
