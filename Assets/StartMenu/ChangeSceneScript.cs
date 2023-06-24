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
                inputType = panel.GetComponent<ToggleInputScript>().GetCurrentInputType(),
                playerIndex = i + 1
            };
            print("Player loaded");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
