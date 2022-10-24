using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : ItemGrab
{
    int ItemCount;

    [SerializeField] private List<Transform> itemsPositions;

    private GameObject currentItem;


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
}

