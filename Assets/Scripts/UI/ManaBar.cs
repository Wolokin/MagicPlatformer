using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : Bar
{
    public int regenRate;

    private void ManaRegen()
    {
        UpdateValue(1);
    }
    new public void Start()
    {
        base.Start();
        InvokeRepeating(nameof(ManaRegen), 0, 1f/regenRate);
    }
}
