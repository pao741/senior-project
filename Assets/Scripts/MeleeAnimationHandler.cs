using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationHandler : MonoBehaviour
{
    // Handle all animation event

    public PlayerCombatManager combatManager;
    PlayerMovement playerMovement;
    public GameObject gameObject;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //combatManager = gameObject.GetComponent<PlayerCombatManager>();
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
