using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public GameObject parentInteractingMessage;
    public TextMeshProUGUI actionText;
    static GameObject interactingMessage;
    static TextMeshProUGUI staticActionText;

    public static GameObject[] items;
    public static int healthCount;
    public static int batteryCount;

    // Start is called before the first frame update
    void Start()
    {
        interactingMessage = parentInteractingMessage;
        staticActionText = actionText; 

        parentInteractingMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetItem();
        Debug.Log(batteryCount);
        GameObject item = getItemInRange();
        if (item != null)
        {
            Battery battery = item.GetComponent<Battery>();
            HealthPlus healthPlus = item.GetComponent<HealthPlus>();
            Portal portal = item.GetComponent<Portal>();
            if (battery != null)
            {
                parentInteractingMessage.SetActive(true);
                actionText.text = battery.message;
            }
            else if (healthPlus != null)
            {
                parentInteractingMessage.SetActive(true);
                actionText.text = healthPlus.message;
            }
            else if (portal != null)
            {
                parentInteractingMessage.SetActive(true);
                actionText.text = portal.message;
            }
        }
        else
        {
            parentInteractingMessage.SetActive(false);
        }

    }

    public GameObject getItemInRange()
    {
        int totalBattery = 0;
        int totalHealth = 0;
        items = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < items.Length; i++)
        {
            Battery currentBattery = items[i].GetComponent<Battery>();
            HealthPlus healthPlus = items[i].GetComponent<HealthPlus>();
            Portal portal = items[i].GetComponent<Portal>();

            if (currentBattery != null)
            {
                totalBattery++;
                if (currentBattery.CheckPlayerInRange())
                {
                    return items[i];
                }
            }
            else if (healthPlus != null)
            {
                totalHealth++;
                if (healthPlus.CheckPlayerInRange())
                {
                    return items[i];
                }
            }
            else if (portal != null)
            {
                if (portal.CheckPlayerInRange())
                {
                    return items[i];
                }
            }
        }
        return null;
    }

    public void SetItem()
    {
        int totalBattery = 0;
        int totalHealth = 0;
        items = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < items.Length; i++)
        {
            Battery currentBattery = items[i].GetComponent<Battery>();
            HealthPlus healthPlus = items[i].GetComponent<HealthPlus>();

            if (currentBattery != null)
            {
                totalBattery++;
            }
            else if (healthPlus != null)
            {
                totalHealth++;
            }
        }

        batteryCount = totalBattery;
        healthCount = totalHealth;
    }


    public static void SetInteractingMessageActive(bool cond)
    {
        interactingMessage.SetActive(cond);
    }

    public static void SetInteractingMessageText(string text)
    {
        staticActionText.text = text;
    }

    public static bool GetInteractingMessageActive()
    {
        return interactingMessage.activeSelf;
    }
}
