﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * scrollSpeed;
        float zMovement = Input.GetAxis("Vertical") * scrollSpeed;

        this.transform.Translate(new Vector3 (xMovement, 0.0f, zMovement));
    }
}