using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Spell : MonoBehaviour
{
    public float speed;
    public int manaCost;
    public float damage;
    public float ttl;

    public TextMeshPro floatingTextPrefab;

    [HideInInspector]
    public string castedBy;

    private Vector3 moveDirection;
    private float deathTime;
    private void Start()
    {
        moveDirection = transform.rotation * Vector3.left;
        moveDirection = Vector3.Normalize(moveDirection);
        deathTime = Time.time + ttl;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * moveDirection;
        if (Time.time > deathTime)
            Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponentInChildren<HealthBar>().UpdateValue((int)-damage);
            TextMeshPro floatingText = Instantiate(floatingTextPrefab, collider.transform.position, Quaternion.identity);
            floatingText.text = "-" + damage;
            floatingText.color = Color.red;
            collider.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        }
        Destroy(gameObject);
    }
}
