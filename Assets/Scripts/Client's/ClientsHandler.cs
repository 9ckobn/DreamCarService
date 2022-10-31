using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsHandler : MonoBehaviour
{
    [SerializeField] private Transform[] SpotsToSpawn;

    [SerializeField] private CarAI[] CarAIPrefabs;

    void Start()
    {
        var routine = Spawner();

        StartCoroutine(routine);
    }

    IEnumerator Spawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);

            int spotIndex = Random.Range(0, SpotsToSpawn.Length);
            int clientIndex = Random.Range(0, CarAIPrefabs.Length);

            var Car = Instantiate(CarAIPrefabs[clientIndex], SpotsToSpawn[spotIndex].position, SpotsToSpawn[spotIndex].rotation, null);
        }
    }
}
