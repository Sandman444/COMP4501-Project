using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    LineRenderer laserLine;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }


    public void SetLaserMat(Material laserMat)
    {
        rend.material = laserMat;
    }
}
