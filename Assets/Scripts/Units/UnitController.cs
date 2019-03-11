using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    //TEMP: Remove when unit selector projector works
    public Material highlightColor;
    private Material normalColor;

    public GameObject unitSelector;

    //Variables
    public bool playerUnit;
    public bool selected;

    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameController>();
        if (game == null)
        {
            Debug.Log("Error: Gamecontroller not attached to a unit");
        }
        selected = false;
        if(transform.parent.name == "Player Units")
        {
            playerUnit = true;
        }
        else
        {
            playerUnit = false;
        }

        //TEMP
        normalColor = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected == false && this.GetComponent<Renderer>().material == highlightColor)
        {
            //TEMP
            this.GetComponent<Renderer>().material = normalColor;
        }

        //deselect other objects
    }

    private void OnMouseDown()
    {
        selected = true;

        //TEMP
        if (playerUnit == true)
        {
            this.GetComponent<Renderer>().material = highlightColor;
        }
    }
}
