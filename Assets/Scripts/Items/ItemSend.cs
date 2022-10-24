using System.Collections;
using UnityEngine;

public class ItemSend : MonoBehaviour
{
    [SerializeField] ItemType itemType;

    IEnumerator Send;
    bool ifExited;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();

            foreach (var item in player.currentItemsArray)
            {
                Debug.Log("Iteration with checking if list exists with current type");

                if (item.itemType == itemType)
                {
                    Debug.Log("list with type is exists, start coroutine");
                    ifExited = false;
                    Send = ItemSender(item, player);
                    StartCoroutine(Send);
                    break;
                }
            }
        }
    }

    void OnTriggerExit() => ifExited = true;

    IEnumerator ItemSender(PlayerCurrentItems playerCurrentItems, Player player)
    {
        for (int i = 0; i < playerCurrentItems.currentCountOfItems - 1; i++)
        {
            if (!ifExited)
            {
                Debug.Log(playerCurrentItems.currentCountOfItems);

                yield return new WaitForSeconds(1);

                playerCurrentItems.currentCountOfItems--;

                Debug.Log(itemType + " on hands " + playerCurrentItems.currentCountOfItems);
            }
        }

        yield break;
    }
}

