using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireballExplosion : MonoBehaviour
{
    public int damage;
    public float lingerTime;

    public TextMeshPro floatingTextPrefab;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("EXPLOSION");
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponentInChildren<HealthBar>().UpdateValue(-damage);
            TextMeshPro floatingText = Instantiate(floatingTextPrefab, collider.transform.position, Quaternion.identity);
            floatingText.text = "-" + damage;
            floatingText.color = Color.red;
            collider.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        }
    }

    public void Update()
    {
        lingerTime -= Time.deltaTime;
        if (lingerTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
