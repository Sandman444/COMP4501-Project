using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAV_Attack : Attack
{
    //GameObject attackTarget;
    public Transform model, barrel, turret;
    Quaternion lookAtDirection;
    LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        attackTarget = null;
        lookAtDirection = Quaternion.identity;
        model = transform.Find("bomber_model_comp");
        barrel = model.Find("Barrel");
        turret = model.Find("Turret");
        laser = GetComponent<LineRenderer>();
    }

    public void AttackAction()
    {
        Debug.Log("LAV Attack!");
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, attackTarget.transform.position);
        lookAtDirection = Quaternion.LookRotation(transform.position - attackTarget.transform.position);
        if (barrel.rotation != lookAtDirection)
        {
            barrel.rotation = lookAtDirection;
        }
    }
}
