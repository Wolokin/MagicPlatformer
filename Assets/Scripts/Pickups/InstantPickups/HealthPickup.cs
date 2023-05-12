using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : InstantPickup
{
    public int healthAmount;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<HealthBar>().UpdateValue(healthAmount);
    }
}
