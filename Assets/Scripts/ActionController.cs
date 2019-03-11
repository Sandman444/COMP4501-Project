using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ActionController : MonoBehaviour
{

    private NavMeshAgent navAgent;

    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void MoveUnit(Vector3 destination)
    {
        navAgent.destination = destination;
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }
}
