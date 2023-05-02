using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaOfEffectDamage : MonoBehaviour//interact case
{

    [SerializeField] private Effects _Effect;

    private List<GameObject> UnderEffects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Effect = GetComponent<Effects>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAreas();
        for (int i = 0; i < UnderEffects.Count; i++)
        {
            AreaEffectInfo _AreaEffectInfo;
            InteractiveObject _InteractiveObject;
            AIHear _AIHear;
            if (UnderEffects[i].TryGetComponent<AreaEffectInfo>(out _AreaEffectInfo))
                _Effect.AddEffect(_AreaEffectInfo.Info);
            if (UnderEffects[i].TryGetComponent<InteractiveObject>(out _InteractiveObject))
                _InteractiveObject.DoSomething();
            if (UnderEffects[i].TryGetComponent<AIHear>(out _AIHear))
                _AIHear.HearTarget(true);
        }
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
        UnderEffects.Clear();
        for (float z = -0.3f; z < 0.31f; z += 0.3f)
            for (float x = -0.3f; x < 0.31f; x += 0.3f)
            {
                
                Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(x, -1, z)), transform.TransformDirection(new Vector3(0, 3, 0)), Color.red);
                hits = Physics.RaycastAll(transform.position + transform.TransformDirection(new Vector3(x, -1, z)), transform.TransformDirection(new Vector3(0, 3, 0)), dist, layerMask);
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
    }

    private void LateUpdate()
    {
        //_Effect.AddEffect(3000, -3);
        //stats.ChangeHP(-1);
    }
}
