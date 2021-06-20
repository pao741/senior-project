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
        ammoText.text = shootingManager.bulletText;
        ammoBar.SetAmmo(10);
        ammoBar.SetMaxAmmo(10);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ShootingManager.currentMagazineCount);
        ammoBar.SetAmmo(shootingManager.currentMagazineCount);
        //ammoBar.SetMaxAmmo(ShootingManager.magazineSize);
        ammoText.text = shootingManager.bulletText;
        //Debug.Log(ShootingManager.currentMagazineCount);
        //Debug.Log(ShootingManager.magazineSize);
        //Debug.Log(ShootingManager.bulletText);
    }
}
