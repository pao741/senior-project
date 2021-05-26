using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Handle all animation event

    CombatManager combatManager;
    PlayerMovement playerMovement;
    public GameObject gameObject;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        combatManager = gameObject.GetComponent<CombatManager>();
    }
    void Update()
    {
        
    }

    void triggerAttack()
    {
        combatManager.Attack();
        combatManager.incrementSeq();
        playerMovement.SetAttackState();
    }
}
