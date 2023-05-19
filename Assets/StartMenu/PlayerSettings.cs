using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InputType
{
    Keyboard,
    Controller
}

public class PlayerSetting
{
    public int skinIndex;
    public InputType inputType;
}
public class PlayerSettings : MonoBehaviour
{
    public static PlayerSetting[] playerSettings;
    public static PlayerSettings instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //public void Start()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
}
