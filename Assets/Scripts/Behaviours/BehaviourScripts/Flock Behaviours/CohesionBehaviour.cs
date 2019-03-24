using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =  "Flock/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    //steer agent toward the average position of neighbours
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        //If no neighbours a change in position/orientation not needed
        if (neighbours.Count == 0)
            return Vector2.zero;

        //find the average position of neighbours
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform obj in neighbours)
        {
            cohesionMove += (Vector2)obj.position;
        }
        cohesionMove /= neighbours.Count;

        //create offset using average neighbour position
        cohesionMove -= (Vector2)agent.transform.position;
        //ease in and out of the movement
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}
