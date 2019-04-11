using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Testing Cube
    public Rigidbody testCube;

    //Unit type enums
    public enum UnitType {LAV, ground_fac, Commander, bomber, gunship};
    public enum User {Player, Computer};  //0 is player, 1 is comp

    private GameObject units;
    //TEMP: Check that objects are being made right (ArrayList more efficient)
    private GameObject playerUnits;
    private GameObject computerUnits;

    //TEMP: Colours for player and computer units
    public Material playerFlying;
    public Material playerLand;
    public Material computerFlying;
    public Material computerLand;

	public GameObject player_object;
	public GameObject computer_object;

    //TEMP: Need better way to load prefabs
    public GameObject ground_LAV;
	public GameObject ground_commander;
	public GameObject air_bomber;
	public GameObject ground_spawner;
	public GameObject air_gunship;

    // Start is called before the first frame update
    void Start()
    {
		//TEMP: Remove Testing Cube
		//this.transform.Find("Testing Cube").GetComponent<Renderer>().enabled = false;

        //Setup Unit Storage GameObjects
        units = new GameObject("Units");
        units.transform.SetParent(this.transform);
        playerUnits = new GameObject("Player Units");
        playerUnits.transform.SetParent(units.transform);
        computerUnits = new GameObject("Computer Units");
        computerUnits.transform.SetParent(units.transform);

        //Create Some initial units
		 
		CreateUnit((int)UnitType.ground_fac, (int)User.Player, new Vector3(-123, -2.9f, -165));
		CreateUnit((int)UnitType.Commander, (int)User.Player, new Vector3(-123, 1, -170));

		CreateUnit((int)UnitType.ground_fac, (int)User.Computer, new Vector3(-25, -2.9f, 0));
		CreateUnit((int)UnitType.Commander, (int)User.Computer, new Vector3(0, -3, -15));
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            testCube.GetComponent<Health>().TakeDamage(10);
        }

        //Attack computers units on right click
        GameObject clickedObject;
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray, out hit, 100.0f))
            {
                clickedObject = hit.collider.gameObject;
                if (clickedObject.tag == "Computer")
                {
                    Debug.Log("Unit Attacked: " + clickedObject.tag + " " + clickedObject.name);
                    foreach (Transform child in playerUnits.transform)
                    {
                        if(child.GetComponent<UnitController>().selected == true && child.GetComponent<Attack>() != null)
                        {
                            Debug.Log("Unit Attacking: " + child.tag + " " +child.name);
                            child.GetComponent<Attack>().AttackUnit(clickedObject);
                        }
                    }
                }
            }
        }
    }

    public void CreateUnit(int type, int user, Vector3 position)
    {
        //need to require that the spawned object is a rigidbody
        GameObject unit;

        if(type == (int)UnitType.bomber)
        {
            unit = Instantiate(air_bomber, position, Quaternion.identity) as GameObject;
            unit.transform.SetParent(units.transform);
        }
        else if (type == (int)UnitType.LAV)
        {
            unit = Instantiate(ground_LAV, position, Quaternion.identity) as GameObject;
            unit.transform.SetParent(units.transform);
        }
		else if (type == (int)UnitType.Commander)
		{
			unit = Instantiate(ground_commander, position, Quaternion.identity) as GameObject;
			unit.transform.SetParent(units.transform);
		}
		else if(type == (int)UnitType.ground_fac) {
			unit = Instantiate(ground_spawner, position, Quaternion.identity) as GameObject;
			unit.transform.SetParent(units.transform);
		}
		else if (type == (int)UnitType.gunship) {
			unit = Instantiate(air_gunship, position, Quaternion.identity) as GameObject;
			unit.transform.SetParent(units.transform);
		}
		else
        {
            unit = Instantiate(ground_LAV, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Debug.Log("Error creating a unit");
        }

		unit.GetComponent<Allegiance>().setAllegiance((int)user);

		//set user tag
		if (user == (int)User.Player)
        {
            unit.transform.tag = "Player";
            unit.transform.SetParent(playerUnits.transform);
			unit.GetComponentInChildren<Renderer>().material = playerFlying;
        }
        else if (user == (int)User.Computer)
        {
            unit.transform.tag = "Computer";
            unit.transform.SetParent(computerUnits.transform);
			unit.GetComponentInChildren<Renderer>().material = computerFlying;

			unit.AddComponent<Enemy_AI>();
            unit.GetComponent<Enemy_AI>().ConnectPlayerUnits(playerUnits);
        }
    }
}
