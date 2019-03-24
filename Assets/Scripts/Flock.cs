using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //Prefab for a flock member
    public FlockAgent agentPrefab;
    //List of members of the flock
    public FlockLeader leader;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    public GameObject destSymbol;

    //[Range(10, 500)]
    public int startingCount = 5;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float speed = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float viewRange = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //utility variables
    float squareMaxSpeed;
    float squareViewRange;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    Vector2 moveLeader;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Leader: " + leader.transform.position);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareViewRange = viewRange * viewRange;
        squareAvoidanceRadius = squareViewRange * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        moveLeader = Vector2.zero;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, Random.insideUnitSphere * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                this.transform
            );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move members of the flock
        foreach (FlockAgent agent in agents)
        {
            List<Transform> neighbours = GetNearbyObjects(agent);
            //move agents unless they colide with the box collider behind the leader
            if (!(neighbours.Contains(leader.transform)))
            {
                Vector2 moveAgent = behaviour.CalculateMove(agent, neighbours, this);
                Debug.Log("moveAgent: " + moveAgent);
               // Vector2 followLeader = moveAgent *= speed;

                if (moveAgent.sqrMagnitude > squareMaxSpeed)
                {
                    moveAgent = moveAgent.normalized * maxSpeed; //threshold move to maxSpeed
                }
                agent.Move(moveAgent);
            }
        }

        //move the flock leader
        if (Input.GetMouseButtonDown(1))
        {
            leader.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Moving to: " + leader.destination);
            destSymbol.transform.position = (Vector2)leader.destination;
        }
        leader.Move();
    }

    //Find all other FlockAgents within view range
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> neighbours = new List<Transform>();
        //Get all FlockAgents that are viewable by agent parameter
        Collider2D[] objectColliders = Physics2D.OverlapCircleAll(agent.transform.position, viewRange);
        foreach (Collider2D c in objectColliders)
        {
            if(c != agent.AgentCollider)
            {
                neighbours.Add(c.transform);
            }
        }
        return neighbours;
    }

    //Move the leader
}
