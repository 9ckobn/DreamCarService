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

            Grab = GrabItem(player);

            StartCoroutine(Grab);

        }
    }

    void OnTriggerExit(Collider other)
    {
        StopCoroutine(Grab);
    }

    private IEnumerator GrabItem(Player player)
    {
        var currentItemsList = new PlayerCurrentItems();
        currentItemsList.itemType = itemType;

        int allowedCount = player.playerConfig.AllowedCountOfItems(itemType);

        for (int i = 0; i < allowedCount; i++)
        {
            if (IsCanGrab(allowedCount))
            {
                ItemOnHandsCount++;
                currentItemsList.currentCountOfItems = ItemOnHandsCount;

                Debug.Log("Grab" + itemType.ToString());

                yield return new WaitForSeconds(1);
            }
        }

        AddToListCurrentItems(currentItemsList, player);

        yield break;
    }

    private void AddToListCurrentItems(PlayerCurrentItems playerCurrentItems, Player player)
    {
        if (!player.playerCurrentItems.Exists(item => item.itemType == itemType)) //если такого списка нет вообще
        {
            player.playerCurrentItems.Add(playerCurrentItems);
        }
        else if (player.playerCurrentItems.Exists(item => item.itemType == itemType &&
        item.currentCountOfItems < player.playerConfig.AllowedCountOfItems(itemType))) //если такой есть и в нем меньше, чем разрешено
        {
            player.playerCurrentItems.Remove(player.playerCurrentItems.Single(item => item.itemType == itemType));
            player.playerCurrentItems.Add(playerCurrentItems);
        }
        else //во всех остальных случаях
        {
            Debug.Log("Player also have maximum items of type " + itemType);
        }
    }

    private bool IsCanGrab(int count)
    {
        if (ItemOnHandsCount != count)
            return true;
        else
            return false;
    }
}

