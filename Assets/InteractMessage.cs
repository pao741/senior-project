using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractMessage : MonoBehaviour
{
    //public static InteractMessage Instance;
    static TextMeshProUGUI actionText;
    static GameObject interactMessageGameObject;
    // Start is called before the first frame update
    void Start()
    {
        //Instance = this;
        interactMessageGameObject = gameObject;
        actionText = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        interactMessageGameObject = gameObject;
    }

    public static void UpdateText(string text)
    {
        actionText.text = text;
    }

    public static void EnableSetActive()
    {
        interactMessageGameObject.SetActive(true);
    }

    public static void DisableSetActive()
    {
        interactMessageGameObject.SetActive(false);
    }

    public static void SetActive(bool cond)
    {
        interactMessageGameObject.SetActive(cond);
    }
}
