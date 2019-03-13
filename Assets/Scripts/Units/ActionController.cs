﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ActionController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    bool selected;
    public Material highlightColor;
    private Material normalColor;

    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void MoveUnit(Vector3 destination)
    {
        navAgent.destination = destination;
    }
    public void unSelect()
    {
        selected = false;
    }
   
}
