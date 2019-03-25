using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 10.0f;
    public float factor = 5.0f;
    public Vector3 cameraAngle = new Vector3(45, 0, 0);
    public float panBordertThickness = 2f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        factor = GetComponent<Camera>().fieldOfView / 60f * 10.0f;
        if (Input.GetKey("up") || Input.mousePosition.y >= Screen.height - panBordertThickness)
        {
            pos.z += scrollSpeed * Time.deltaTime * factor;
        }
        if (Input.GetKey("down") || Input.mousePosition.y <= panBordertThickness)
        {
            pos.z -= scrollSpeed * Time.deltaTime * factor;
        }
        if (Input.GetKey("right") || Input.mousePosition.x >= Screen.width - panBordertThickness)
        {
            pos.x += scrollSpeed * Time.deltaTime * factor;
        }
        if (Input.GetKey("left") || Input.mousePosition.x <= panBordertThickness)
        {
            pos.x -= scrollSpeed * Time.deltaTime * factor;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView++;
        }
        transform.position = pos;
    }
}