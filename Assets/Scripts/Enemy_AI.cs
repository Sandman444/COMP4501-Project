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
    float timeToNewWander = 1.0f;
    float timer = 0;
    Vector3 wanderDest;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
        ac = this.GetComponent<ActionController>();
        uc = this.GetComponent<UnitController>();
        attack = this.GetComponent<Attack>();
        target = null;
        wanderDest = new Vector3(Random.Range(-1.0f, 1.0f) * 500, 0, Random.Range(-1.0f, 1.0f) * 500);
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
            //Debug.Log("Lost target");
        }

        if (anim.GetBool("isAttacking") == true)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= attack.range)
            {
                ac.MoveUnit(target.transform.position);
            }
        }
        else if (anim.GetBool("isSearching") == true)
        {
            //wander around searching for an enemy unit
            timer += Time.deltaTime;
            if (timer > timeToNewWander)
            {
                wanderDest = new Vector3(Random.Range(-1.0f, 1.0f) * 500, 0, Random.Range(-1.0f, 1.0f) * 500);
                timer = 0;
                timeToNewWander = Random.Range(0.4f, 2.5f);
                Debug.Log("new direction");
            }
            ac.MoveUnit(wanderDest);

        }
    }

    public void ConnectPlayerUnits(GameObject units)
    {
        playerUnits = units;
    }
}
