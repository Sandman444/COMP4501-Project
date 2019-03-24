using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : ScriptableObject
{
    public abstract Vector2 SteeringMove(FlockAgent agent, Vector2 currentVelocity, Vector2 destination, float maxSpeed = 5f, float maxForce = Mathf.Infinity);
}

