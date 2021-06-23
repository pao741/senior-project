using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public GameObject parentInteractingMessage;
    public static GameObject interactingMessage;
    public static TextMeshProUGUI actionText;

    // Start is called before the first frame update
    void Start()
    {
        //interactingMessage = GameObject.Find("Canvas/InGameUI/InteractingMessage/InteractMessageText");
        //actionText = GameObject.Find("Canvas/InGameUI/InteractingMessage/Action").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(interactingMessage == null);
        //Debug.Log(actionText == null);
    }

    public static GameObject GetInteractingMessage()
    {
        return interactingMessage;
    }

    public static TextMeshProUGUI GetActionText()
    {
        return actionText;
    }
}
