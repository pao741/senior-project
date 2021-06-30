using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlus : MonoBehaviour
{
    // Start is called before the first frame update
    public float interactingRange = 1f;
    public Animator healthAnimator;
    public string message = "pick up Health+";
    public GameObject refillTextUIPrefab;

    private bool interactable = true;
    //private bool justSet = false;

    void Start()
    {
    }

    void Update()
    {
        if (CheckPlayerInRange() && interactable)
        {
            healthAnimator.SetBool("inRange", true);

            if (Input.GetKeyDown(KeyCode.E) && Player.Heal())
            {
                //also give player battery
                GameObject refillText = Instantiate(refillTextUIPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                Destroy(refillText, 2f);
                Destroy();

            }
        }
        else
        {
            healthAnimator.SetBool("inRange", false);
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
