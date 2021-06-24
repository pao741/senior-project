using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        Debug.Log("Setting Max Health Value");
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        Debug.Log("Setting Health Value");
        slider.value = health;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
