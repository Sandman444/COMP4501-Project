using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int range, minRange;
    public int damage;
    public float attackTime = 0.5f;
    float timer = 0;

    GameObject attackTarget;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        attackTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget != null)
        {
            Debug.DrawLine(transform.position, attackTarget.transform.position);
        }
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            AttackUnit(attackTarget);
            timer = 0;
        }
    }

    public void AttackUnit(GameObject enemy)
    {
        if (attackTarget != null)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null && enemyHealth.currentHealth > 0 && TestHit() == true)
            {
                bool targetAlive = enemy.GetComponent<Health>().TakeDamage(damage);
                if(targetAlive == false)
                {
                    attackTarget = null;
                }
            }
        }
    }

    public void setTarget(GameObject target)
    {
        attackTarget = target;
    }

    bool TestHit()
    {
        return true;
    }
}
