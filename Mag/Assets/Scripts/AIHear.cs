using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHear : MonoBehaviour
{
    [SerializeField] private EnemyAI _EnemyAI;
    public void HearTarget(bool Flag)
    {
        _EnemyAI.HearTarget = Flag;
    }
}
