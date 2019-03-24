using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    //align agent with neighbour orientations
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        //If no neighbours maintain current orientation
        if (neighbours.Count == 0)
            return Vector2.zero;

        //find the average position of neighbours within avoidance radius
        Vector2 avoidanceMove = Vector2.zero;
        int avoidNumber = 0;
        foreach (Transform obj in neighbours)
        {
            if(Vector2.SqrMagnitude(obj.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {         
                avoidanceMove += (Vector2)(agent.transform.position - obj.position); //move away from their average position
                avoidNumber++;
            }
        }
        if(avoidNumber > 0)
        {
            avoidanceMove /= avoidNumber;
        }

        return avoidanceMove;
    }
}
