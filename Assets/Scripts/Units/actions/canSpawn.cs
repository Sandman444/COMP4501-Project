using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canSpawn : MonoBehaviour
{
	public int target;
	public bool isSpawning;
	private GameObject game;
	float cooldown;
	int maxCooldown;

	//the options that this thing could make
	public int obj1;
	public int obj2;
	public int obj3;
	public int obj4;

	// Start is called before the first frame update
	public void Start()
    {
		maxCooldown = 6;
		cooldown = maxCooldown;
		isSpawning = true;
		target = obj1;
		game = GameObject.FindGameObjectWithTag("game_tag");
		
	}

    // Update is called once per frame
    public void Update() {
        if (isSpawning && target > -1) {///{LAV, ground_fac, Commander, bomber};
			
			if (cooldown <= 0) {
				cooldown = maxCooldown;
				game.GetComponent<GameController>().CreateUnit(target, this.GetComponentInParent<Allegiance>().allegiance, this.transform.position);
				Debug.Log("Spawned id " + this.GetComponentInParent<Allegiance>().allegiance);
			}
			else {
				
			}
			cooldown -= Time.deltaTime;
		}
		if (this.GetComponentInParent<Allegiance>().allegiance == 0) {
			if (Input.GetKey(KeyCode.Alpha1)) {   //Engi			---		Engi
				target = obj1;
			}
			if (Input.GetKey(KeyCode.Alpha2)) {   //LAV			---		Bomber
				target = obj2;
			}
			if (Input.GetKey(KeyCode.Alpha3)) {   //Tank			---		Gunship
				target = obj3;
			}
			if (Input.GetKey(KeyCode.Alpha4)) {   //Pepsi man		---		Other flying thing
				target = obj4;
			}
			if (Input.GetKey(KeyCode.Q)) {
				isSpawning = !isSpawning;
				if (isSpawning) {
					transform.parent.gameObject.GetComponentInChildren<Animator>().SetTrigger("build");
				}
				else {
					transform.parent.gameObject.GetComponentInChildren<Animator>().SetTrigger("idle");
				}

				cooldown = maxCooldown;

			}
		}
		
	}

}
