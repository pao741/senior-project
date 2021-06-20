using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUIManager : MonoBehaviour
{
    public GameObject meleeManagerObject;
    public GameObject shootingManagerObject;
    public AmmoBar ammoBar;
    PlayerCombatManager meleeManager;
    Shooting shootingManager;

    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        meleeManager = meleeManagerObject.GetComponent<PlayerCombatManager>();
        shootingManager = shootingManagerObject.GetComponent<Shooting>();
        //ammoBar.SetMaxAmmo(shootingManager.magazineSize);
        //ammoBar.SetMaxAmmo(10);
        ammoText.text = shootingManager.bulletText;
        //SetRangeBulletUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shootingManager.magazineSize);
        if (meleeManagerObject.activeSelf)
        {
            //Debug.Log("Melee");
        }
        else
        {
            ammoBar.SetAmmo(shootingManager.currentMagazineCount);
            //SetRangeBulletUI();
        }
        ammoText.text = shootingManager.bulletText;
    }

    void SetRangeBulletUI()
    {
        ammoBar.SetAmmo(shootingManager.currentMagazineCount);
        //ammoBar.SetMaxAmmo(10);
        //ammoBar.SetMaxAmmo(shootingManager.magazineSize);
        //Debug.Log(shootingManager.magazineSize == 10);
    }

    void SetMeleeDurabilityUI()
    {
        //ammoBar.SetAmmo(shootingManager.currentMagazineCount);
        //ammoBar.SetMaxAmmo(100);
    }
}
