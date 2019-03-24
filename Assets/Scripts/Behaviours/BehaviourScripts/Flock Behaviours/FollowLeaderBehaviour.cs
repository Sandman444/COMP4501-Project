using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/FollowLeader")]
public class FollowLeaderBehaviour : FlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    //steer agent toward the average position of neighbours
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        //create offset using leader position
        Vector2 followMove = (Vector2)flock.leader.transform.position - (Vector2)agent.transform.position;
        Debug.DrawRay((Vector2)agent.transform.position, followMove);
        //move to leader position
        followMove = Vector2.SmoothDamp(agent.transform.up, followMove, ref currentVelocity, agentSmoothTime);

        return followMove;
    }
}
