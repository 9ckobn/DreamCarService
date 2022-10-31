using Newtonsoft.Json;
using UnityEngine;

public class InGameShopHandler : MonoBehaviour
{
    public BuyedObject buyedObject;

    void Start()
    {
        if (buyedObject == null)
        {
            Debug.Log("Shop is not inizialized!");
            try
            {
                Debug.Log("Read from device");
                buyedObject = JsonConvert.DeserializeObject<BuyedObject>(WritePlayerData.DecodedString("BuyedObjects.json"));
            }
            catch
            {
                Debug.Log("creating new");
                buyedObject = new BuyedObject();
            }
        }
    }

    void OnApplicationQuit()
    {
        WritePlayerData.WriteObject(buyedObject, "BuyedObjects.json");
    }
}
