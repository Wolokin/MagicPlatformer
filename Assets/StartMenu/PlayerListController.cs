using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListController : MonoBehaviour
{
    public int startingLength;
    public int maxLength;
    public GameObject playerPanel;
    public GameObject addButtonPanel;

    private int length;

    public void Start()
    {
        length = 0;
        for (int i = 0; i < startingLength; i++)
        {
            OnAdd();
            if(i%2 == 0)
            {
                transform.GetChild(i).GetComponent<ToggleInputScript>().OnControllerToggle(true);
            }
            else
            {
                transform.GetChild(i).GetComponent<ToggleInputScript>().OnKeyboardToggle(true);
            }
        }
    }

    public void OnAdd()
    {
        if (length < maxLength)
        {
            length++;
            GameObject panel = Instantiate(playerPanel);
            panel.transform.SetParent(transform, false);
            panel.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + length;
            addButtonPanel.transform.SetAsLastSibling();
        }
        if (length == maxLength)
        {
            addButtonPanel.SetActive(false);
        }
    }
}
