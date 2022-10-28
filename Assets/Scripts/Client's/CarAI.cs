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
        navMeshAgent.speed = 50;

        navMeshAgent.acceleration = 30;

        navMeshAgent.angularSpeed = 300;

        navMeshAgent.radius = 2.45f;

        navMeshAgent.avoidancePriority = 60;

        foreach (var item in spotsHandler.AllSpots)
            if (item.isAvailable)
                destinationPoint = item.gameObject.transform.position;

        if (destinationPoint == Vector3.zero)
        {
            Debug.Log("Here not available points!");
            return;
        }

        var routine = GetDestination(destinationPoint);

        StartCoroutine(routine);
    }

    private IEnumerator GetDestination(Vector3 lastPoint)
    {
        Vector3 tempPoint = new Vector3(lastPoint.x, 0, transform.position.z);

        navMeshAgent.SetDestination(tempPoint);

        while (IsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }

        navMeshAgent.SetDestination(lastPoint);

        yield break;
    }

    private bool IsMoving()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
