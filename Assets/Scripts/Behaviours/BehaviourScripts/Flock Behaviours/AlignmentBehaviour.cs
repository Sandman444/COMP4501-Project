using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    //align agent with neighbour orientations
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        //If no neighbours maintain current orientation
        if (neighbours.Count == 0)
            return agent.transform.up;

        //find the average orientation of neighbours
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform obj in neighbours)
        {
            alignmentMove += (Vector2)obj.transform.up;
        }
        alignmentMove /= neighbours.Count;

        return alignmentMove;
    }
}
