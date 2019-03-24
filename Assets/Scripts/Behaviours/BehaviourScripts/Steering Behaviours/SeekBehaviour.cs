using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Steer/Seek")]
public class SeekBehaviour : SteeringBehaviour
{
    public override Vector2 SteeringMove(FlockAgent agent, Vector2 currentVelocity, Vector2 destination, float maxSpeed = 5f, float maxForce = Mathf.Infinity)
    {
        //get the two velocity vectors used for steering
        Vector2 position = (Vector2)agent.transform.position;
        Vector2 desired_velocity = (destination - position).normalized * maxSpeed;
        Vector2 steering = desired_velocity - currentVelocity;

        //convert to force
        steering /= agent.mass;
        steering = Vector2.ClampMagnitude(steering, maxForce);
        Vector2 velocity = Vector2.ClampMagnitude(currentVelocity + steering, maxSpeed);
        return velocity;
    }
}
