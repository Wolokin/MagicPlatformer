using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    private int currentIndex;

    public void OnDropdownChange(int index)
    {
        currentIndex = index;
        image.sprite = sprites[index];
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
