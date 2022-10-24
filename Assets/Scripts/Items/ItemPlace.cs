using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
public class ItemPlace : MonoBehaviour
{
    [Header("Item configurator")]
    [SerializeField] ItemType itemType;

    int ItemCount;
    int ItemOnHandsCount;

    [SerializeField] private List<Transform> itemsPositions;

    private List<Item> ItemList;
    private GameObject currentItem;

    IEnumerator Grab;

    bool ifExited;

    void Start()
    {
        ItemCount = itemsPositions.Capacity;

        ItemList = new List<Item>(ItemCount);

        foreach (var item in Game.itemData.prefab)
            if (item.GetComponent<Item>().itemType == itemType)
                currentItem = item;


        foreach (var item in itemsPositions)
        {
            var prefab = Instantiate(currentItem, item);
            ItemList.Add(prefab.GetComponent<Item>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();

            ifExited = false;

            Grab = GrabItem(player);

            StartCoroutine(Grab);
        }
    }

    void OnTriggerExit() => ifExited = true;

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
                ItemOnHandsCount++;
                currentItemsList.currentCountOfItems = ItemOnHandsCount;

                yield return new WaitForSeconds(1);
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

