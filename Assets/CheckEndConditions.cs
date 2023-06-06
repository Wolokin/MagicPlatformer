using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckEndConditions : MonoBehaviour
{
    public float checkInterval = 1.0f;
    public GameObject victoryRoyaleCanvasPrefab;

    private GameObject playersParent = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckForEndConditions", 0.0f, checkInterval);

    }

    private void InstantiateVictoryRoyale()
    {
        GameObject canvasObj = Instantiate(victoryRoyaleCanvasPrefab);
        Image image = canvasObj.GetComponentInChildren<Image>();

        SpriteRenderer sprite = playersParent.GetComponentInChildren<SpriteRenderer>();
        if (sprite != null)
        {
            image.sprite = sprite.sprite;
        }
    }

    private void CheckForEndConditions()
    {
        if (playersParent == null)
        {
            playersParent = GameObject.Find("Players");
        }
        else
        {
            if (playersParent.transform.childCount <= 1)
            {
                CancelInvoke("CheckForEndConditions");
                InstantiateVictoryRoyale();
            }
        }
    }
}
