using UnityEngine;
using UnityEngine.UI;
using static TypeConfigurator;

public class WishHandler : MonoBehaviour
{
    public Image CurrentWish;
    public GameObject WishImageObject;

    private ItemType _wishType;
    public ItemType wishType
    {
        get
        {
            return _wishType;
        }
        set
        {
            _wishType = value;
            ChangeSprite(_wishType);
            WishImageObject.SetActive(true);
        }
    }

    public ItemSpend InitSpender()
    {
        if (GetComponent<ItemSpend>() == null)
            return gameObject.AddComponent<ItemSpend>();

        else return GetComponent<ItemSpend>();
    }

    public void ChangeSprite(ItemType itemType)
    {
        CurrentWish.sprite = TypeConfigurator.GetSprite(wishType);

        CurrentWish.SetNativeSize();
    }
}
