using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //setup move
        Vector2 move = Vector2.zero;

        //iterate through all attached behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 behaviourMove = behaviours[i].CalculateMove(agent, neighbours, flock) * weights[i];

            if (behaviourMove != Vector2.zero)
            {
                if (behaviourMove.sqrMagnitude > (weights[i] * weights[i]))
                {
                    behaviourMove.Normalize();
                    behaviourMove *= weights[i];
                }

                move += behaviourMove;
            }
        }

        return move;
    }
}
