using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaPickup : Pickup
{
    public int manaAmount;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<ManaBar>().UpdateValue(manaAmount);
        var tmp = FloatingText("+" + manaAmount + " Mana");
        tmp.color = Color.blue;
    }
}
