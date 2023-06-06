using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : Bar
{
    public new void Update()
    {
        base.Update();
        if(currSliderValue <= 0)
        {
            if (GameObject.Find("Players").transform.childCount > 1)
            {
                Destroy(transform.parent.parent.gameObject);
            }
        }
    }
}
