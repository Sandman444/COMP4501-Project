using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]
public class FlockAgent : MonoBehaviour
{
    //Collider to find neighbours in flock
    Collider2D agentCollider;

    public float mass;
    public Collider2D AgentCollider { get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        mass = 5f;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
