using System.Collections;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject MoneyPrefab;

    public void GenerateMoneys(Vector3 generatePosition)
    {
        int maxCountsOfItems = Random.Range(5, 8);

        StartCoroutine(PauseAtSpawnings(maxCountsOfItems, generatePosition));
    }

    IEnumerator PauseAtSpawnings(int counts, Vector3 generatePosition)
    {   
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < counts; i++)
        {
            var money = Instantiate(MoneyPrefab, generatePosition, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            EventListener.OnMoneyGenerated();
        }
    }
}
