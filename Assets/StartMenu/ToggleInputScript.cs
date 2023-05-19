using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInputScript : MonoBehaviour
{
    public Toggle controller;
    public Toggle keyboard;

    public void OnControllerToggle(bool value)
    {
        if (value)
        {
            keyboard.isOn = false;
        }
        else
        {
            keyboard.isOn = true;
        }
    }

    public void OnKeyboardToggle(bool value)
    {
        if (value)
        {
            controller.isOn = false;
        }
        else
        {
            controller.isOn = true;
        }
    }

    public InputType GetCurrentInputType()
    {
        if (controller.isOn)
        {
            return InputType.Controller;
        }
        else
        {
            return InputType.Keyboard;
        }
    }
}
