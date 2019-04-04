using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public int currentResources;
	public int maxResources;
	public Text resText;
	public bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
		currentResources = 2000;
		maxResources = 2500;
    }

	void Update() {
		resText.text = "resources: " + currentResources;
	}

	public void addResources(int r) {
		currentResources += r;
		if(currentResources >= maxResources) {
			currentResources = maxResources;
		}
	}

	public void subtractResources(int r) {
		currentResources -= r;
		if(currentResources <= 0) {
			currentResources = 0;
		}
	}
}
