using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockLeader : FlockAgent
{
    public SteeringBehaviour steering;

    Vector2 dest;
    BoxCollider2D bc;
    BoxCollider2D BC { get  { return bc; } }

    [HideInInspector]
    public Vector2 velocity, destination;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
        destination = Vector2.zero;
        bc = GetComponent<BoxCollider2D>();
    }

    public void Move()
    {
        if (!destination.Equals(Vector2.zero))
        {
            velocity = steering.SteeringMove(this, velocity, destination);
            this.Move(velocity);
        }
    }
}
