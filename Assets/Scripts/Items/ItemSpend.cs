using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static TypeConfigurator;

public class ItemSpend : MonoBehaviour
{
    public ItemType itemType;

    IEnumerator Send;
    Player player;

    public bool ThisIsTrashCan;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();

            if (!ThisIsTrashCan)
                foreach (var item in player.currentItemsArray)
                {
                    if (item.itemType == itemType)
                    {
                        Send = ItemSender(item);
                        StartCoroutine(Send);
                        break;
                    }
                }
            else
            {
                var Send = TrashSender(player);
                StartCoroutine(Send);
            }
        }
    }

    void OnTriggerExit()
    {
        try
        {
            if (!ThisIsTrashCan) StopCoroutine(Send);
        }
        catch { }
    }

    IEnumerator TrashSender(Player player)
    {
        while (player.AllItems.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);

            GameObject neededItem = null;
            if (player.AllItems.Count - 1 < 0)
                break;

            neededItem = player.AllItems[player.AllItems.Count - 1].gameObject;

            if (neededItem == null)
            {
                Debug.Log("Needed item is empty");
                break;
            }

            ItemFly itemFly = new ItemFly()
            {
                _player = player,
                currentItem = neededItem
            };


            itemFly.SendObject(transform.position);
        }

        try
        {
            foreach (var item in player.currentItemsArray)
                player.currentItemsArray.Remove(item);
        }
        catch { }
    }

    IEnumerator ItemSender(PlayerCurrentItems playerCurrentItems)
    {
        if (playerCurrentItems.currentCountOfItems > 0)
        {
            yield return new WaitForSeconds(1);
            playerCurrentItems.currentCountOfItems--;

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

