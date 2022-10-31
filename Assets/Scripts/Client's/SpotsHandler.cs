using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotsHandler : MonoBehaviour
{
    [SerializeField] private PayToEarn BuyMechanism;
    public Spot[] AllSpots;

    float priceIndex = 2f;
    int pastPrice;

    void Start()
    {
        pastPrice = 50;

        foreach(var item in AllSpots)
        {
            if((GameDependencies.instance._inGameShop.buyedObject.ID_Price.ContainsKey(item.ID)))
            {
                item.gameObject.SetActive(true);
                pastPrice = GameDependencies.instance._inGameShop.buyedObject.ID_Price.GetValueOrDefault(item.ID);
            }
            else
            {   
                var buyMech = Instantiate(BuyMechanism, item.transform.position, Quaternion.identity, gameObject.transform);
                buyMech.ObjectTobuy = item;
                buyMech.Price = (int)(pastPrice * priceIndex);
                pastPrice = buyMech.Price;
            }
        }
    }

}
