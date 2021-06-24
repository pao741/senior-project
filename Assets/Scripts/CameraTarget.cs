using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Camera cam;
    public float threshold;

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused || Player.isDead)
        {
            return;
        }
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (Player.GetPosition() + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + Player.GetPosition().x, threshold + Player.GetPosition().x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + Player.GetPosition().y, threshold + Player.GetPosition().y);

        this.transform.position = targetPos;
    }
}
