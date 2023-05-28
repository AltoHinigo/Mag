using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaOfEffectDamage : MonoBehaviour//interact case
{

    [SerializeField] private Effects _Effects = null;

    private List<GameObject> Past = new List<GameObject>();
    private List<GameObject> New = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(_Effects == null)
            _Effects = GetComponent<Effects>();
    }

    private void Awake()
    {
        //Effect = GetComponent<Effects>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAreas();

        /*string str = "";
        for (int i = 0; i < New.Count; i++)
            str += New[i].name;
        Debug.Log(str);*/

        int p = 0;
        for (int i = 0; i < New.Count; i++)
        {
            AreaEffectInfo _AreaEffectInfo;
            InteractiveObject _InteractiveObject;
            AIHear _AIHear;
            if (New[i].TryGetComponent<InteractiveObject>(out _InteractiveObject))
            {
                p = Past.IndexOf(New[i]);
                if (p == -1)
                {
                    if (_InteractiveObject.OnEnter != null)
                        _InteractiveObject.OnEnter(gameObject);
                }
                else
                {
                    Past.RemoveAt(p);
                    if (_InteractiveObject.OnInside != null)
                        _InteractiveObject.OnInside(gameObject);
                }
            }
            else if (New[i].TryGetComponent<AIHear>(out _AIHear))
            {
                _AIHear.HearTarget(true);
            }
        }
        
        for (int i = 0; i < Past.Count; i++)
        {
            AreaEffectInfo _AreaEffectInfo;
            InteractiveObject _InteractiveObject;
            AIHear _AIHear;

            if (Past[i].TryGetComponent<InteractiveObject>(out _InteractiveObject))
                if (_InteractiveObject.OnLeave != null)
                    _InteractiveObject.OnLeave(gameObject);

        }
        /*string str = "";
        for (int i = 0; i < Past.Count; i++)
            str += Past[i].name;
        Debug.Log("Past" + str);*/
        Past = new List<GameObject>(New);
        //Debug.Log(UnderEffects[i].name);
        //Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0, -1, 0)), transform.TransformDirection(new Vector3(0, 3, 0)), Color.red);
    }

    void GetAreas()
    {
        LayerMask layerMask = 1 << 9;
        float dist = 30.0f;
        bool flag = false;
        RaycastHit[] hits;
        int hit = 0;

        if (Past.Count != 0)
        {
            New.Clear();
            if (Past.Count == 0)
                Debug.LogError("New очистило Past!");
        }
        else
            New.Clear();
        for (float z = -0.3f; z < 0.31f; z += 0.3f)
            for (float x = -0.3f; x < 0.31f; x += 0.3f)
            {
                
                Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(x, -1, z)), transform.TransformDirection(new Vector3(0, 3, 0)), Color.red);
                hits = Physics.RaycastAll(transform.position + transform.TransformDirection(new Vector3(x, -1, z)), transform.TransformDirection(new Vector3(0, 3, 0)), dist, layerMask);
                if (hits.Length != 0)
                {
                    while (hit < hits.Length)
                    {
                        for (int MortalObject = 0; MortalObject < New.Count; MortalObject++)
                        {
                            if (New[MortalObject].GetHashCode() == hits[hit].collider.gameObject.GetHashCode())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            New.Add(hits[hit].collider.gameObject);
                        }
                        else
                            flag = false;
                        hit++;
                    }
                }
            }
    }
}
