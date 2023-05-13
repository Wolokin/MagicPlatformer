using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bar : MonoBehaviour
{
    public Slider barSlider;
    public int min;
    public int max;
    public int curr;
    public int currSliderValue;
    public int timeToFill;
    private float nextUpdate;
    public void UpdateValue(int update)
    {
        curr = Mathf.Clamp(curr + update, min, max);
    }
    public void Start()
    {
        nextUpdate = Time.time;
    }
    public void Update()
    {
        if (currSliderValue != curr && nextUpdate < Time.time)
        {
            currSliderValue += (curr > currSliderValue) ? 1 : -1;
            nextUpdate = Time.time + (float)timeToFill / max;
        }
        barSlider.value = currSliderValue;
    }
}