using Newtonsoft.Json;
using UnityEngine;

public class InGameShopHandler : MonoBehaviour
{
    public BuyedObject buyedObject;

    void Start()
    {
        if (buyedObject == null)
        {
            try
            {
                buyedObject = JsonConvert.DeserializeObject<BuyedObject>(WritePlayerData.DecodedString("BuyedObjects.json"));
            }
            catch
            {
                buyedObject = new BuyedObject();
            }
        }
    }

    void OnApplicationQuit()
    {
        WritePlayerData.WriteObject(buyedObject, "BuyedObjects.json");
    }
}
