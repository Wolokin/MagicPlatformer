using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosionPrefab;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
    }
}
