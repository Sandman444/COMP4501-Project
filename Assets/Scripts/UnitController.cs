using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    //TEMP: Remove when unit selector projector works
    public Material highlightColor;
    private Material normalColor;

    public GameObject unitSelector;
    private bool selected;
    private bool playerUnit; 

    // Start is called before the first frame update
    void Start()
    {
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
