using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Steer/Wander")]
public class WanderBehaviour : SteeringBehaviour
{
    public float wanderAngle;

    public override Vector2 SteeringMove(FlockAgent agent, Vector2 currentVelocity, Vector2 destination, float maxSpeed = 5f, float maxForce = Mathf.Infinity)
    {
        float theta = Random.Range(-wanderAngle, wanderAngle);
        return Vector2.zero;
    }
}
