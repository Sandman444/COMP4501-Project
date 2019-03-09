using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
    public Rigidbody landVehicle;
    public Rigidbody flyingVehicle;

    // Start is called before the first frame update
    void Start()
    {
        //Setup Unit Storage GameObjects
        units = new GameObject();
        playerUnits = new GameObject();
        playerUnits.transform.SetParent(units.transform);
        computerUnits = new GameObject();
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

    }

    void CreateUnit(UnitType type, User user, Vector3 position)
    {
        //need to require that the spawned object is a rigidbody
        switch (type)
        {
            case UnitType.Flying:
                //NOTE: Convert this section to generic method later
                Rigidbody flyingObj = (Rigidbody)Instantiate(flyingVehicle, position, flyingVehicle.transform.rotation);
                flyingObj.transform.SetParent(units.transform);
                if (user == User.Player)
                {
                    //playerUnits.Add(flyingObj);
                    flyingObj.transform.SetParent(playerUnits.transform);
                    flyingObj.GetComponent<Renderer>().material = playerFlying;
                }
                else if (user == User.Computer)
                {
                    //computerUnits.Add(flyingObj);
                    flyingObj.transform.SetParent(computerUnits.transform);
                    flyingObj.GetComponent<Renderer>().material = computerFlying;
                }
                //End Note
                return;
            case UnitType.Land:
                Rigidbody landObj = (Rigidbody)Instantiate(landVehicle, position, Quaternion.identity);
                landObj.transform.SetParent(units.transform);
                if (user == User.Player)
                {
                    //playerUnits.Add(landObj);
                    landObj.transform.SetParent(playerUnits.transform);
                    landObj.GetComponent<Renderer>().material = playerFlying;
                }
                else if (user == User.Computer)
                {
                    //computerUnits.Add(landObj);
                    landObj.transform.SetParent(computerUnits.transform);
                    landObj.GetComponent<Renderer>().material = computerFlying;
                }
                return;
            default:
                Debug.Log("Error creating a unit");
                return;
        }
    }
}
