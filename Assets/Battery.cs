using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    public float interactingRange = 1f;

    public GameObject interactingMessage;
    public TextMeshProUGUI actionText;
    private string message = "pick up battery";

    private bool interactable = true;

    void Start()
    {

    }

    void Update()
    {
        if (CheckPlayerInRange() && interactable)
        {
            interactingMessage.SetActive(true);

            actionText.text = message;
            if (Input.GetKeyDown(KeyCode.E))
            {
                //also give player battery
                Player.Refill();
                interactingMessage.SetActive(false);
                Destroy();

            }
        }
        else
        {
            interactingMessage.SetActive(false);
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
        float distanceFromPlayer = Vector3.Distance(Player.getPosition(), transform.position);
        return distanceFromPlayer <= interactingRange;
    }
}
