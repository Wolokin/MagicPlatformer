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
        // Create empty parent
        GameObject players = new GameObject("Players");

        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();
        foreach (PlayerSetting playerSetting in PlayerSettings.playerSettings)
        {
            playerInputManager.playerPrefab = playerPrefabs[playerSetting.skinIndex];
            PlayerInput playerInput = null;
            if (playerSetting.inputType == InputType.Controller)
            {
                playerInput = playerInputManager.JoinPlayer(controlScheme: "Gamepad", pairWithDevice: null);
            }
            else if (playerSetting.inputType == InputType.Keyboard)
            {
                playerInput = playerInputManager.JoinPlayer(controlScheme: "Keyboard&Mouse", pairWithDevice: Keyboard.current);
            }
            if (playerInput != null)
            {
                playerInput.transform.parent.transform.SetParent(players.transform, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
