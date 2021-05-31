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

    void Attack() // called when attack
    {
        enemyAI.Attack();
        EnableAttacking();
    }

    void EnableAttacking() // called when start attacking
    {
        enemyAI.SetAttacking(true);
    }

    void DisableAttacking()
    {
        enemyAI.SetAttacking(false);
    }

    void DisableMovement()
    {
        enemyAI.SetDisableMovement(true);
    }

    void EnableMovement()
    {
        enemyAI.SetDisableMovement(false);
    }
}
