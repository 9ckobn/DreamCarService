using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    [HideInInspector]
    public List<Item> ItemList;

    [Header("Item configurator")]

    public ItemType itemType;

    IEnumerator Grab;
    Player player;
    bool ifExited;

    int ItemOnHandsCount;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();

            ifExited = false;

            if (ItemList != null && ItemList.Count > 0)
            {
                Grab = GrabItem(player);

                StartCoroutine(Grab);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ifExited = true;
        }
    }

    private IEnumerator GrabItem(Player player)
    {
        PlayerCurrentItems currentItemsList;

        if (!ArrayExist(player))
        {
            currentItemsList = new PlayerCurrentItems();
            currentItemsList.itemType = itemType;
            currentItemsList.currentCountOfItems = 0;
            ItemOnHandsCount = 0;
        }
        else
        {
            currentItemsList = player.currentItemsArray.Single(item => item.itemType == itemType);
            ItemOnHandsCount = currentItemsList.currentCountOfItems;
        }

        int allowedCount = player.playerConfig.AllowedCountOfItems(itemType);

        for (int i = currentItemsList.currentCountOfItems; i < allowedCount; i++)
        {
            if (IsCanGrab(allowedCount) && !ifExited)
            {
                Debug.Log("Grab item " + itemType);

                ItemFly itemFly = new ItemFly()
                {
                    _player = player,
                    currentItem = ItemList[ItemList.Count - 1].gameObject
                };

                ItemOnHandsCount++;
                currentItemsList.currentCountOfItems = ItemOnHandsCount;

                itemFly.GetObject();

                ItemList.Remove(ItemList[ItemList.Count - 1]);

                yield return new WaitForSeconds(player.playerConfig._timeToGetItem);
            }
        }

        AddToListCurrentItems(currentItemsList, player);

        yield break;
    }

    private void AddToListCurrentItems(PlayerCurrentItems playerCurrentItems, Player player)
    {
        if (!ArrayExist(player))
        {
            player.currentItemsArray.Add(playerCurrentItems);
        }
        else if (player.currentItemsArray.Exists(item => item.itemType == itemType &&
        item.currentCountOfItems < player.playerConfig.AllowedCountOfItems(itemType)))
        {
            player.currentItemsArray.Remove(player.currentItemsArray.Single(item => item.itemType == itemType));
            player.currentItemsArray.Add(playerCurrentItems);
        }
        else
        {
            Debug.Log("Player also have maximum items of type " + itemType);
        }
    }

    private bool IsCanGrab(int count)
    {
        if (ItemOnHandsCount < count)
            return true;
        else
            return false;
    }

    private bool ArrayExist(Player player) => player.currentItemsArray.Exists(item => item.itemType == itemType);
}
