using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(WishHandler))]
public class CarAI : MonoBehaviour
{
    private SpotsHandler spotsHandler;
    private WishHandler _wishHandler;

    NavMeshAgent navMeshAgent;

    private Spot spot;

    Vector3 destinationPoint;
    Vector3 startPoint;

    public Transform tempSpot;
    public Transform secondTempSpot;

    void Start()
    {
        //NavMeshHit closestHit;

        //if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        // {
        //transform.position = closestHit.position;
        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        //}

        _wishHandler = GetComponent<WishHandler>();

        spotsHandler = GameDependencies.instance._spotsHandler;

        startPoint = transform.position;

        Move();
    }

    [ContextMenu("Move")]
    public void Move()
    {
        navMeshAgent.speed = 50;

        navMeshAgent.acceleration = 15;

        navMeshAgent.angularSpeed = 280;

        navMeshAgent.radius = 2.45f;

        navMeshAgent.avoidancePriority = 60;

        foreach (var item in spotsHandler.AllSpots)
            if (item.isFree && item.gameObject.activeSelf)
            {
                spot = item;
                spot.isFree = false;

                destinationPoint = item.gameObject.transform.position;

                break;
            }

        if (spot == null)
        {
            Debug.Log("Here not available points!");

            StartCoroutine(ViolentActivity());

            return;
        }

        var routine = GetDestinationToService(destinationPoint);

        StartCoroutine(routine);
    }

    private IEnumerator ViolentActivity()
    {
        Vector3 tempPoint;

        if (transform.position.z > 0)
        {
            tempPoint = new Vector3(transform.position.x, 0, transform.position.z - 500);
        }
        else
        {
            tempPoint = new Vector3(transform.position.x, 0, transform.position.z + 500);
        }

        navMeshAgent.SetDestination(tempPoint);

        while (IsMoving())
        {
            yield return new WaitForSeconds(2);
        }

        Destroy(gameObject);
    }

    private IEnumerator GetDestinationToService(Vector3 lastPoint)
    {
        navMeshAgent.SetDestination(tempSpot.position);

        while (IsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }


        navMeshAgent.SetDestination(secondTempSpot.position);

        while (IsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }


        navMeshAgent.SetDestination(lastPoint);

        while (IsMoving())
        {
            yield return new WaitForSeconds(1f);
        }

        var clientWishes = new ClientWishes(_wishHandler);

        yield break;
    }

    private IEnumerator Getback(Vector3 lastPoint)
    {
        navMeshAgent.angularSpeed = 360;

        navMeshAgent.SetDestination(secondTempSpot.position);

        while (IsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }


        navMeshAgent.SetDestination(tempSpot.position);

        while (IsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }

        navMeshAgent.angularSpeed = 300;

        spot.isFree = true;

        navMeshAgent.SetDestination(lastPoint);

        while (IsMoving())
        {
            yield return new WaitForSeconds(1f);
        }

        Destroy(gameObject);

        yield break;
    }

    public void GetServiced()
    {
        _wishHandler.WishImageObject.SetActive(false);

        Destroy(GetComponent<ItemSpend>());

        var routine = Getback(startPoint);
        StartCoroutine(routine);


        GameDependencies.instance._moneyGenerator.GenerateMoneys(destinationPoint + (Vector3.up * 3));
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
