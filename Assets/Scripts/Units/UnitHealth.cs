using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    //Public Variables
    public int startingHealth = 25;
    public int currentHealth = 25;

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
    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        //kill if health is zero
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    //Kill the Unit if health reaches zero
    void Kill()
    {
        Destroy(gameObject);
    }
}
