using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waypoint;

    void Start()
    {
        //GetComponenet<>
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomManager.getIsCleared()) {
            float distance = Vector2.Distance(Player.GetPosition(), Portal.getPosition());
            if (distance < 2f)
            {
                waypoint.SetActive(false);
            }
            else
            {
                waypoint.SetActive(true);
            }
        }
    }

    public void setActiveWaypoint(bool cond)
    {
        waypoint.SetActive(cond);
    }
}
