using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healthAmount;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<HealthBar>().UpdateValue(healthAmount);
        var tmp = FloatingText("+" + healthAmount + " HP");
        tmp.color = Color.red;
    }
}
