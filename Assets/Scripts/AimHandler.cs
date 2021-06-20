using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] aims;
    public GameObject[] animators;
    PlayerCombatManager meleeManager;
    Shooting shootingManager;
    private int aimIndex;


    void Start()
    {
        aimIndex = 0;
        aims[0].SetActive(true);
        animators[0].SetActive(true);
        for (int i = 1; i < aims.Length; i++)
        {
            aims[i].SetActive(false);
            animators[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mouseScrollDelta.y);
        if (Input.mouseScrollDelta.y > 0f)
        {
            aims[aimIndex].SetActive(false);
            animators[aimIndex].SetActive(false);
            aimIndex--;
            if (aimIndex < 0)
            {
                aimIndex = aims.Length - 1;
            }
            aims[aimIndex].SetActive(true);
            animators[aimIndex].SetActive(true);
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            aims[aimIndex].SetActive(false);
            animators[aimIndex].SetActive(false);
            aimIndex = (aimIndex + 1) % aims.Length;
            aims[aimIndex].SetActive(true);
            animators[aimIndex].SetActive(true);
        }
    }

    public void Refill()
    {
        meleeManager = aims[aimIndex].GetComponent<PlayerCombatManager>();
        shootingManager = aims[aimIndex].GetComponent<Shooting>();
        if (meleeManager)
        {

        }
        else
        {
            shootingManager.Refill();
        }
    }
}
