using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Transform> wayPoints;

 
    public int nextIdx = 0;


    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;

        var group = GameObject.Find("WayPointGroup");

        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }

        MoveWayPoint();
    }

    private void MoveWayPoint()
    {

        if (agent.isPathStale)
        {
            return;
        }

       
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (/*agent.velocity.sqrMagnitude >= 0.2f * 0.2f &&*/
          agent.remainingDistance <= 0.5f)
        {
             
            nextIdx = Random.Range(0, wayPoints.Count);
          
            MoveWayPoint();
        }
    }
}
