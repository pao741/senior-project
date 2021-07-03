using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUIManager : MonoBehaviour
{
    GameObject meleeManagerObject;
    GameObject shootingManagerObject;
    public AmmoBar ammoBar;
    public TextMeshProUGUI weaponName;
    PlayerCombatManager meleeManager;
    Shooting shootingManager;

    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        /*shootingManager = Player.GetShootingManager();
        shootingManagerObject = Player.GetShootingManagerGameObject();
        meleeManager = Player.GetMeleeManager();
        meleeManagerObject = Player.GetMeleeManagerGameObject();*/

        //ammoText.text = shootingManager.bulletText;
    }

    // Update is called once per frame
    void Update()
    {
        shootingManager = Player.GetShootingManager();
        shootingManagerObject = Player.GetShootingManagerGameObject();
        meleeManager = Player.GetMeleeManager();
        meleeManagerObject = Player.GetMeleeManagerGameObject();
        if (meleeManagerObject != null && meleeManagerObject.activeSelf)
        {
            SetMeleeDurabilityUI();
            ammoText.text = meleeManager.durabilityText;
            weaponName.text = meleeManager.name;
        }
        else
        {
            SetRangeBulletUI();
            ammoText.text = shootingManager.bulletText;
            weaponName.text = shootingManager.name;
        }
    }

    void SetRangeBulletUI()
    {
        ammoBar.SetMaxAmmo(shootingManager.magazineSize);
        ammoBar.SetAmmo(shootingManager.currentMagazineCount);
    }

    void SetMeleeDurabilityUI()
    {
        ammoBar.SetMaxAmmo(meleeManager.maxDurability);
        ammoBar.SetAmmo(meleeManager.currentDurability);
    }
}
