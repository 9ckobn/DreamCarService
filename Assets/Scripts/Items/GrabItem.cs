using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
public class GrabItem : MonoBehaviour
{
    [Header("Item configurator")]
    [SerializeField] ItemType itemType;

    int ItemCount;

    [SerializeField] private List<Transform> itemsPositions;

    private List<Item> ItemList;
    private GameObject currentItem;

    void Start()
    {
        ItemCount = itemsPositions.Capacity;

        ItemList = new List<Item>(ItemCount);

        // foreach(var item in Game.itemPrefabData.prefab)
        //     if(item.GetComponent<Item>().itemType == itemType)
        //         currentItem = item;


        // foreach(var item in itemsPositions)
        // {
        //     var prefab = Instantiate(currentItem, item);
        // }

        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Here grabbing " + itemType.ToString());

                foreach(var item in Game.itemData.prefab)
            if(item.GetComponent<Item>().itemType == itemType)
                currentItem = item;


        foreach(var item in itemsPositions)
        {
            var prefab = Instantiate(currentItem, item);
        }
    }
}

