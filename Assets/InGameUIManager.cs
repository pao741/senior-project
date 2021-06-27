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

    // Start is called before the first frame update
    void Start()
    {
        interactingMessage = parentInteractingMessage;
        staticActionText = actionText; 
        //interactingMessage = FindObject("InteractMessage");
        //interactingMessage = parentInteractingMessage;
        //actionText = interactingMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();


        //interactingMessage = GameObject.Find("/Canvas(Clone)/InGameUI/InteractMessage/");
        //actionText = GameObject.Find("/Canvas(Clone)/InGameUI/InteractMessage/Action/").GetComponent<TextMeshProUGUI>();

        parentInteractingMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject item = getItemInRange();
        if (item != null)
        {
            Battery battery = item.GetComponent<Battery>();
            Portal portal = item.GetComponent<Portal>();
            if (battery != null)
            {
                parentInteractingMessage.SetActive(true);
                actionText.text = battery.message;
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
        GameObject[] items = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < items.Length; i++)
        {
            Battery currentBattery = items[i].GetComponent<Battery>();
            Portal portal = items[i].GetComponent<Portal>();

            if (currentBattery != null)
            {
                if (currentBattery.CheckPlayerInRange())
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
