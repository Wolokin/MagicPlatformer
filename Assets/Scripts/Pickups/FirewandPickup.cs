using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirewandPickup : Pickup
{
    public GameObject spellPrefab;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<SpellSource>().spellPrefab = spellPrefab;
        var tmp = FloatingText("+ Fire wand");
        tmp.color = new Color(1f, 0.5f, 0f, 1f);
    }
}
