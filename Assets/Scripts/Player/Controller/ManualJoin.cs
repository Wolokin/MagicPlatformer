using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualJoin : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();
        foreach (PlayerSetting playerSetting in PlayerSettings.playerSettings)
        {
            playerInputManager.playerPrefab = playerPrefabs[playerSetting.skinIndex];
            if (playerSetting.inputType == InputType.Controller)
            {
                playerInputManager.JoinPlayer(controlScheme: "Gamepad", pairWithDevice: null);
            }
            else if (playerSetting.inputType == InputType.Keyboard)
            {
                playerInputManager.JoinPlayer(controlScheme: "Keyboard&Mouse", pairWithDevice: Keyboard.current);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
