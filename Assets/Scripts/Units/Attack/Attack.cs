using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int range;
    public int damage;

    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackUnit(GameObject enemy)
    {
        Debug.Log("Attacking");
        if (enemy.GetComponent<Health>() != null && TestHit() == true)
        {
            enemy.GetComponent<Health>().TakeDamage(damage);
        }

    }
    bool TestHit()
    {
        return true;
    }
}
