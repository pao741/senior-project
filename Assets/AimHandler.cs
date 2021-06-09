using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] aims;
    private int aimIndex;


    void Start()
    {
        aimIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mouseScrollDelta.y);
        if (Input.mouseScrollDelta.y > 0f)
        {
            Debug.Log("Up");
            aims[aimIndex].SetActive(false);
            aimIndex -= 1;
            aims[aimIndex].SetActive(true);
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            Debug.Log("Down");
            aims[aimIndex].SetActive(false);
            aimIndex += 1;
            aims[aimIndex].SetActive(true);
        }
    }
}
