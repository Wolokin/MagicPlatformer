using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpeed : Pickup
{
    public float speedIncrease;

    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<SpellSource>().spellSpeedIncrease *= (1+speedIncrease);
        var tmp = FloatingText("+" + speedIncrease*100 + "% Spell Speed");
        tmp.color = new Color(0f, 1f, 1f, 1f);
    }
}
