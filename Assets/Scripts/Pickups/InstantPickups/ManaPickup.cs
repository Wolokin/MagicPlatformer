using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : InstantPickup
{
    public int manaAmount;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<ManaBar>().UpdateValue(manaAmount);
    }
}
