using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PayToEarn : MonoBehaviour, IBuyable
{
    public BuyableObject ObjectTobuy;
    [SerializeField] private Text _buyPlacePrice;

    public int Price;
    private int MoneyLeft
    {
        get { return Price; }
        set
        {
            Price = value;
            _buyPlacePrice.text = @"$" + value.ToString();

        }
    }

    void Start()
    {
        MoneyLeft = Price;
        ObjectTobuy.Price = Price;
    }

    IEnumerator routine;

    public void OnTriggerEnter(Collider other)
    {
        Player player;
        if (other.TryGetComponent<Player>(out player))
        {
            routine = BuyProcess();
            StartCoroutine(routine);
        }

    }

    public IEnumerator BuyProcess()
    {
        yield return new WaitForSeconds(1);

        int tempLeft = MoneyLeft - (int)(MoneyLeft / 4);

        while (MoneyLeft > tempLeft)
        {
            yield return new WaitForSeconds(0.025f);
            MoneyLeft--;
            GameDependencies.instance._statsHandler.moneyCounter.MoneyCount--;
        }

        while (MoneyLeft > 0)
        {
            yield return new WaitForSeconds(0.001f);
            MoneyLeft--;
            GameDependencies.instance._statsHandler.moneyCounter.MoneyCount--;
        }

        GetItem(ObjectTobuy);
    }

    public bool IsEnoughMoney()
    {
        if (GameDependencies.instance._statsHandler.moneyCounter.MoneyCount > 0)
            return true;
        else
            return false;
    }

    public void GetItem(BuyableObject buyableObject)
    {
        buyableObject.gameObject.SetActive(true);
        GameDependencies.instance._inGameShop.buyedObject.ID_Price.Add(buyableObject.ID, buyableObject.Price);
        //GameDependencies.instance._inGameShop.buyedObject.BuyedObjects.Add(buyableObject.ID);
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        Player player;
        if (other.TryGetComponent<Player>(out player))
        {
            if (!IsEnoughMoney())
            {
                StopCoroutine(routine);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player player;
        if (other.TryGetComponent<Player>(out player))
        {
            StopCoroutine(routine);
        }
    }
}
