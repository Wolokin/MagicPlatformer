using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeSceneScript : MonoBehaviour
{
    public GameObject settingsPanel;

    public PlayerSettings playerSettings;
    public void ChangeScene(string sceneName)
    {
        int length = settingsPanel.transform.childCount - 1;
        PlayerSettings.playerSettings = new PlayerSetting[length];
        for (int i = 0; i < length; i++)
        {
            GameObject panel = settingsPanel.transform.GetChild(i).gameObject;
            PlayerSettings.playerSettings[i] = new PlayerSetting
            {
                skinIndex = panel.GetComponentInChildren<ChangeImage>().GetCurrentIndex(),
                inputType = panel.GetComponent<ToggleInputScript>().GetCurrentInputType()
            };
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
