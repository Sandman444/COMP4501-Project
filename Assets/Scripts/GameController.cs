using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Testing Cube
    public Rigidbody testCube;

    //Unit type enums
    public enum UnitType {Land, Flying};
    public enum User {Player, Computer};

    private GameObject units;
    //TEMP: Check that objects are being made right (ArrayList more efficient)
    private GameObject playerUnits;
    private GameObject computerUnits;

    //TEMP: Colours for player and computer units
    public Material playerFlying;
    public Material playerLand;
    public Material computerFlying;
    public Material computerLand;

    //TEMP: Need better way to load prefabs
    public Rigidbody landUnit;
    public Rigidbody flyingUnit;

    //Load actual unit models below


    // Start is called before the first frame update
    void Start()
    {
        //TEMP: Remove Testing Cube
        //this.transform.Find("Testing Cube").GetComponent<Renderer>().enabled = false;

        //Setup Unit Storage GameObjects
        units = new GameObject("Units");
        playerUnits = new GameObject("Player Units");
        playerUnits.transform.SetParent(units.transform);
        computerUnits = new GameObject("Computer Units");
        computerUnits.transform.SetParent(units.transform);

        //Create Some initial units
        CreateUnit(UnitType.Land, User.Player, new Vector3(0, 1, 0));
        CreateUnit(UnitType.Land, User.Computer, new Vector3(0, 1, 7));
        CreateUnit(UnitType.Flying, User.Player, new Vector3(0, 7.5f, 0));
        CreateUnit(UnitType.Flying, User.Computer, new Vector3(0, 7.5f, 7));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            testCube.GetComponent<UnitHealth>().TakeDamage(10);
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
            }
        }
        //if (clickedObject.ta)
    }

    void CreateUnit(UnitType type, User user, Vector3 position)
    {
        //need to require that the spawned object is a rigidbody
        Rigidbody unit;

        if(type == UnitType.Flying)
        {
            unit = (Rigidbody)Instantiate(flyingUnit, position, flyingUnit.transform.rotation);
            unit.transform.SetParent(units.transform);
        }
        else if (type == UnitType.Land)
        {
            unit = (Rigidbody)Instantiate(landUnit, position, Quaternion.identity);
            unit.transform.SetParent(units.transform);
        }
        else
        {
            unit = (Rigidbody)Instantiate(landUnit, new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log("Error creating a unit");
        }

        //set user tag
        if (user == User.Player)
        {
            unit.transform.tag = "Player";
            unit.transform.SetParent(playerUnits.transform);
            unit.GetComponent<Renderer>().material = playerFlying;
        }
        else if (user == User.Computer)
        {
            unit.transform.tag = "Computer";
            unit.transform.SetParent(computerUnits.transform);
            unit.GetComponent<Renderer>().material = computerFlying;
        }
    }

}
