using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    List<ActionController> selectedUnits = new List<ActionController>();
    RaycastHit hit;
    bool isDragging = false;
    Vector3 mousePosition;

    private void OnGUI()
    {
        if (isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect,new Color(0.8f,0.8f,0.95f,0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            //Create a ray from the camera to the space
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(camRay,out hit))
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.CompareTag("PlayerUnit"))
                {
                    SelectUnit(hit.transform.gameObject.GetComponent<ActionController>(), Input.GetKey(KeyCode.LeftShift));
                }
                else
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            UnSelectUnits();
            foreach(var selectableObject in FindObjectsOfType<BoxCollider>())
            {
                if (isWithinSelectionBound(selectableObject.transform)&&selectableObject.gameObject.tag=="PlayerUnit")
                {
                    SelectUnit(selectableObject.gameObject.GetComponent<ActionController>(), true);
                }
            }

            isDragging = false;
        }

        if (Input.GetMouseButtonDown(1)&&selectedUnits.Count>0)
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out hit))
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.CompareTag("Terrain"))
                {
                    foreach(var selectableObj in selectedUnits)
                    {
                        selectableObj.MoveUnit(hit.point);
                    }
                }
            }
        }
    }

    private void SelectUnit(ActionController unit,bool isMultiSelect = false)
    {
        if (!isMultiSelect)
        {
            UnSelectUnits();
        }
        selectedUnits.Add(unit);
        unit.SetSelected(true);

    }

    private void UnSelectUnits()
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            
            selectedUnits[i].SetSelected(false);
        }
        selectedUnits.Clear();
    }

    private bool isWithinSelectionBound(Transform transform)
    {
        if (!isDragging)
        {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = ScreenHelper.GetViewportBounds(camera,mousePosition,Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
    }
}
