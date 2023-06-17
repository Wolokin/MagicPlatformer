using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualJoin : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    private void JoinPlayers()
    {

        // Create empty parent
        GameObject players = new GameObject("Players");

        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.EnableJoining();
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

    // Start is called before the first frame update
    public void Start()
    {
        // wait 1s
        Invoke(nameof(JoinPlayers), 0.5f);


    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
