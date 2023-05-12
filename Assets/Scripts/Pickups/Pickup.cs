using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickup : MonoBehaviour
{
    public float floatingHeightMultiplier;
    public float floatingSpeedMultiplier;

    private float floatingHeight;
    private float randomPhase;

    public abstract void OnPickup(Collider2D other);

    public void PickupAndDespawn(Collider2D other)
    {
        OnPickup(other);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupAndDespawn(other);
        }
    }

    public void Start()
    {
        floatingHeight = GetComponent<BoxCollider2D>().size.y * floatingHeightMultiplier;
        randomPhase = Random.Range(0, 2 * Mathf.PI);
    }

    public void Update()
    {
        transform.position = transform.parent.position;


        // Make the pickup float up and down
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(randomPhase + Time.time * floatingSpeedMultiplier) * floatingHeight, transform.position.z);
    }
}