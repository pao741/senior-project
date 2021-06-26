using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    public float interactingRange = 1f;
    public Animator batteryAnimator;
    public GameObject interactingMessage;
    public TextMeshProUGUI actionText;
    private string message = "pick up battery";
    public GameObject refillTextUIPrefab;

    private bool interactable = true;
    //private bool justSet = false;

    void Start()
    {
    }

    void Update()
    {
        if (CheckPlayerInRange() && interactable && !InGameUIManager.GetInteractingMessageActive())
        {
            batteryAnimator.SetBool("inRange", true);
            InGameUIManager.SetInteractingMessageActive(true);

            InGameUIManager.SetInteractingMessageText(message);

            if (Input.GetKeyDown(KeyCode.E) && Player.Refill())
            {
                //also give player battery

                InGameUIManager.SetInteractingMessageActive(false);
                GameObject refillText = Instantiate(refillTextUIPrefab, transform.position + new Vector3(0,0.5f,0), transform.rotation);
                Destroy(refillText, 2f);
                Destroy();

            }
        }
        else if (!CheckPlayerInRange() && InGameUIManager.GetInteractingMessageActive())
        {
            batteryAnimator.SetBool("inRange", false);
            InGameUIManager.SetInteractingMessageActive(false);
        }
    }
    

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetInteractable(bool state)
    {
        gameObject.SetActive(state);
        interactable = state;
    }

    public bool CheckPlayerInRange()
    {
        float distanceFromPlayer = Vector3.Distance(Player.GetPosition(), transform.position);
        return distanceFromPlayer <= interactingRange;
    }
}
