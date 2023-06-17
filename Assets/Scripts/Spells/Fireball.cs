using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void Explode()
    {

        explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //explosionPrefab = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Explode();
    }
}
