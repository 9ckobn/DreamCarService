using System.Collections;
using UnityEngine;

public class ItemSpend : MonoBehaviour
{
    [SerializeField] ItemType itemType;

    IEnumerator Send;
    Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();

            foreach (var item in player.currentItemsArray)
            {
                if (item.itemType == itemType)
                {
                    Send = ItemSender(item);
                    StartCoroutine(Send);
                    break;
                }
            }
        }
    }

    void OnTriggerExit() => StopCoroutine(Send);

    IEnumerator ItemSender(PlayerCurrentItems playerCurrentItems)
    {
        if (playerCurrentItems.currentCountOfItems > 0)
        {
            yield return new WaitForSeconds(3);
            playerCurrentItems.currentCountOfItems--;
            Debug.Log(itemType + " was sended");

            GameObject neededItem = null;

            for (int i = player.AllItems.Count; i > 0; i--)
            {
                if (player.AllItems[i - 1].itemType == itemType)
                {
                    neededItem = player.AllItems[i - 1].gameObject;
                    break;
                }
            }

            if (neededItem == null)
            {
                Debug.Log("Needed item is empty");
                yield break;
            }

            ItemFly itemFly = new ItemFly()
            {
                _player = player,
                currentItem = neededItem
            };

            itemFly.SendObject(transform.position);
        }

        yield break;
    }
}

