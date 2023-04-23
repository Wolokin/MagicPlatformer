using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider healthBarSlider;
    public Slider manaBarSlider;

    public int minHealth = 0;
    public int maxHealth = 100;
    public int currHealth;

    public int minMana = 0;
    public int maxMana = 100;
    public int currMana;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;

        currMana = maxMana;
        manaBarSlider.maxValue = maxMana;
        manaBarSlider.value = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        int healthUpdate = 0;
        int manaUpdate = 0;
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            healthUpdate = -10;
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            healthUpdate = 10;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            manaUpdate = -10;
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            manaUpdate = 10;
        }

        currHealth = Mathf.Clamp(currHealth + healthUpdate, minHealth, maxHealth);
        healthBarSlider.value = currHealth;
        currMana = Mathf.Clamp(currMana + manaUpdate, minMana, maxMana);
        manaBarSlider.value = currMana;
    }
}
