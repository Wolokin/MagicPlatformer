using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSource : MonoBehaviour
{
    public GameObject spellPrefab;
    public Vector2 spellSpawnDistance;

    public float spellSpeedIncrease;
    public float spellDamageIncrease;

    private ManaBar manaBar;
    private int manaCost;

    public void Start()
    {
        manaBar = transform.parent.GetComponentInChildren<ManaBar>();
        manaCost = spellPrefab.GetComponent<Spell>().manaCost;
    }
    public void CastSpell (Vector2 dir) 
    {
        if (manaBar.curr < manaCost)
            return;
        manaBar.UpdateValue(-manaCost);
        Vector3 dir3 = new(dir.x*spellSpawnDistance.x, dir.y*spellSpawnDistance.y, 0);
        Spell spell = Instantiate (spellPrefab, transform.position + dir3, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90)* dir3)).GetComponent<Spell>();
        Physics2D.IgnoreCollision(spell.GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
        spell.speed *= spellSpeedIncrease;
        spell.damage *= spellDamageIncrease;
    }
}
