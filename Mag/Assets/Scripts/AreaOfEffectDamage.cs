using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectDamage : MonoBehaviour
{

    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        stats = GetComponent<Stats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        stats.AddEffect(3000, -3);
        //stats.ChangeHP(-1);
    }
}
