using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EternalwandPickup : Pickup
{
    public GameObject spellPrefab;
    public override void OnPickup(Collider2D other)
    {
        other.GetComponentInChildren<SpellSource>().spellPrefab = spellPrefab;
        var tmp = FloatingText("+ Eternal wand");
        tmp.color = new Color(0.5f, 0f, 1f, 1f);
    }
}
