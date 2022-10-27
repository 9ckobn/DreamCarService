using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    [SerializeField] SpotsHandler spotsHandler;

    NavMeshAgent navMeshAgent;

    Vector3 destinationPoint;

    void Start()
    {
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            transform.position = closestHit.position;
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }

    [ContextMenu("Move")]
    public void SetDestinationPoint()
    {

            navMeshAgent.speed = 100;

            navMeshAgent.acceleration = 100;

        foreach (var item in spotsHandler.AllSpots)
            if (item.isAvailable)
                destinationPoint = item.gameObject.transform.position;

        navMeshAgent.SetDestination(destinationPoint);
    }

    public void PathFinding()
    {
        
    }
}
