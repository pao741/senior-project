using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    Transform playerPosition; 
    EnemyAI enemyAI;
    EnemyCombatManager enemyAttackManager;
    public GameObject AttackObject;

    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        enemyAttackManager = AttackObject.GetComponentInParent<EnemyCombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RememberPlayerPosition()
    {
        enemyAI.SetAttackingPosition();
    }

    void Attack() // called when attack
    {
        EnableAttacking();
        enemyAttackManager.SetAttacking(true);
        enemyAI.SetAttacking(true);
        enemyAI.Attack();
    }

    void EnableAttacking() // called when start attacking
    {
        enemyAI.SetAttacking(true);
    }

    void DisableAttacking()
    {
        enemyAI.SetAttacking(false);
        enemyAttackManager.SetAttacking(false);
    }

    void DisableMovement()
    {
        enemyAI.SetDisableMovement(true);
    }

    void EnableMovement()
    {
        enemyAI.SetDisableMovement(false);
    }

    void StopRigidBody()
    {
        enemyAI.StopRigidBody();
    }
}
