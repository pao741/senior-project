using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyAI enemyAI;

    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerAttack()
    {
        enemyAI.Attack();
    }

    void TriggerSetAttacking()
    {
        enemyAI.SetAttacking();
    }
}
