using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    Animator anim;
    ActionController ac;
    Attack attack;
    UnitController uc;
    GameObject playerUnits;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
        ac = this.GetComponent<ActionController>();
        uc = this.GetComponent<UnitController>();
        attack = this.GetComponent<Attack>();
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        //if no target set to searching and continuously look for a new target
        if (target == null)
        {
            if (anim.GetBool("isSearching") == false)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isSearching", true);
            }
            //find a target within range
            foreach (Transform child in playerUnits.transform)
            {
                if (Vector3.Distance(child.transform.position, transform.position) <= uc.viewRange)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isSearching", false);
                    Debug.Log("Seen a player unit");
                    target = child.gameObject;
                    this.GetComponent<Attack>().setTarget(target);
                    break;
                }
            }
        }
        //target moves outside of sight
        else if (Vector3.Distance(target.transform.position, transform.position) >= uc.viewRange)
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isSearching", true);
            target = null;
            this.GetComponent<Attack>().setTarget(target);
            Debug.Log("Lost target");
        }

        if (anim.GetBool("isAttacking") == true)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= attack.range)
            {
                ac.MoveUnit(target.transform.position);
            }
        }
    }

    public void ConnectPlayerUnits(GameObject units)
    {
        playerUnits = units;
    }
}
