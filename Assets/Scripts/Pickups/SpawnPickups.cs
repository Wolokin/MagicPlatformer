using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnPickups : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] pickups;
    public float spawnPeriod;
    public int pickupLimit;
    public float spawnCollisionRadius;

    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;

    void Spawn()
    {
        if(transform.childCount >= pickupLimit)
        {
            return;
        }
        int pickupIndex = Random.Range(0, pickups.Length);
        GameObject pickup = pickups[pickupIndex];
        for (int i = 0; i < 10; i++)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            Collider2D collisionCheck = Physics2D.OverlapCircle(spawnPoint, spawnCollisionRadius, LayerMask.GetMask(new string[]{ "GroundLayer"}));
            if (collisionCheck == false)
            {
                var p = Instantiate(pickup,  new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.identity, transform);
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, spawnPeriod);
        Vector2 worldMin = tilemap.transform.TransformPoint(tilemap.localBounds.min);
        Vector2 worldMax = tilemap.transform.TransformPoint(tilemap.localBounds.max);
        xMin = worldMin.x;
        xMax = worldMax.x;
        yMin = worldMin.y;
        yMax = worldMax.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
