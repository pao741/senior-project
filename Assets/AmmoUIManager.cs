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
    public TextMeshProUGUI weaponName;
    PlayerCombatManager meleeManager;
    Shooting shootingManager;

    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        meleeManager = meleeManagerObject.GetComponent<PlayerCombatManager>();
        shootingManager = shootingManagerObject.GetComponent<Shooting>();
        ammoText.text = shootingManager.bulletText;
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeManagerObject.activeSelf)
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
