using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUI : MonoBehaviour
{
    private double widthPercentage = 0.177;
    private double heightPercentage = 0.111;
    private int referenceWidth = 1920;
    private int referenceHeight = 1080;
    // Start is called before the first frame update
    void Start()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        int left = 0, bottom = 0, right = 0, top = 0;
        int widthPadding = (int)(referenceWidth * (1.0 - widthPercentage));
        int heightPadding = (int)(referenceHeight * (1.0 - heightPercentage));
        switch (playerCount)
        {
            case 1:
                left = widthPadding;
                top = heightPadding;
                break;
            case 2:
                right = widthPadding;
                top = heightPadding;
                break;
            case 3:
                left = widthPadding;
                bottom = heightPadding;
                break;
            case 4:
                right = widthPadding;
                bottom = heightPadding;
                break;
        }
        rt.offsetMin = new Vector2(left, bottom);
        rt.offsetMax = new Vector2(-right, -top);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
