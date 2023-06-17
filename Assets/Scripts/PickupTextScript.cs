using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupTextScript : MonoBehaviour
{
    public float fadeTime = 1f;
    public float floatSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        // Slowly float upwards
        transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
        // Slowly fade away
        Color color = GetComponent<TextMeshPro>().color;
        color.a -= Time.deltaTime / fadeTime;
        GetComponent<TextMeshPro>().color = color;
        // Destroy when invisible
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
