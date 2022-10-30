using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static TypeConfigurator;

public class ItemGrab : MonoBehaviour
{
    [HideInInspector]
    public List<Item> ItemList;

    [Header("Item configurator")]

    public ItemType itemType;

    UniTask Grab;
    Player player;
    bool ifExited;

    int ItemOnHandsCount;

    async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();

            ifExited = false;

            if (ItemList != null && ItemList.Count > 0)
            {
                Grab = GrabItem(player);

                await Grab;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        ifExited = true;

    }

    private async UniTask GrabItem(Player player)
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

        int allowedCount = AllowedCountOfItems(itemType);

        for (int i = currentItemsList.currentCountOfItems; i < allowedCount; i++)
        {
            if (IsCanGrab(allowedCount) && !ifExited)
            {
                ItemFly itemFly = new ItemFly()
                {
                    _player = player,
                    currentItem = ItemList[ItemList.Count - 1].gameObject
                };

                ItemOnHandsCount++;
                currentItemsList.currentCountOfItems = ItemOnHandsCount;

                itemFly.GetObject();

                ItemList.Remove(ItemList[ItemList.Count - 1]);

                await UniTask.Delay(player.playerConfig._timeToGetItemInMS);

                //yield return new WaitForSeconds(player.playerConfig._timeToGetItem);
            }
        }

        AddToListCurrentItems(currentItemsList, player);
    }

    private void AddToListCurrentItems(PlayerCurrentItems playerCurrentItems, Player player)
    {
        if (!ArrayExist(player))
        {
            player.currentItemsArray.Add(playerCurrentItems);
        }
        else if (player.currentItemsArray.Exists(item => item.itemType == itemType &&
        item.currentCountOfItems < AllowedCountOfItems(itemType)))
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
