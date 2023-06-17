using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPanelScript : MonoBehaviour
{
    public void OnRemove()
    {
        PlayerListController playerListController = GetComponentInParent<PlayerListController>();
        playerListController.OnRemove();
        Destroy(gameObject);
    }

}
