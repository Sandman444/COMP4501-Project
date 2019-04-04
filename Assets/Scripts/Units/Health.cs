using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Public Variables
    public int startingHealth = 25;
    public int currentHealth = 25;
    public RectTransform healthBar;

    bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Damage Unit
    public bool TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
    
        //kill if health is zero
        if (currentHealth <= 0)
        {
            Debug.Log("Killing a unit");
            Kill();
            return false;
        }
        else
        {
            healthBar.sizeDelta = new Vector2(100 * ((float)currentHealth / startingHealth), healthBar.sizeDelta.y);
            return true;
        }
    }

    //Kill the Unit if health reaches zero
    void Kill()
    {
        Destroy(gameObject);
    }
}
