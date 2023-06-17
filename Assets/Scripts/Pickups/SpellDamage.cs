using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : Pickup
{
    public float damageIncrease;

    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<SpellSource>().spellDamageIncrease *= (1+damageIncrease);
        var tmp = FloatingText("+" + damageIncrease*100 + "% Spell Damage");
        tmp.color = new Color(214 / 255f, 64 / 255f, 103 / 255f, 1);
    }
}
