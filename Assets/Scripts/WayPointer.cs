using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointer : MonoBehaviour
{

    //public Transform target;
    //private Vector3 targetPosition;

    void Start()
    {
        
    }


    void Awake()
    {
    }

    void Update()
    {
        //RotatePointer();

        //targetPosition = target.position;
        Vector3 toPosition = Portal.GetPosition();
        //Vector3 fromPosition = Camera.main.transform.position;
        Vector3 fromPosition = Player.GetPosition();
        fromPosition.z = 0f;
        Vector3 difference = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) % 360;
        //pointer.localEulerAngles = new Vector3(0, 0, angle);


        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
