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
        //Debug.Log(interactingMessage == null);
        //Debug.Log(actionText == null);
    }

    /*public static GameObject GetInteractingMessage()
    {
        return interactingMessage;
    }

    public static TextMeshProUGUI GetActionText()
    {
        return actionText;
    }*/

    public static void SetInteractingMessageActive(bool cond)
    {
        Debug.Log(cond);
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
