using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
}

public enum ItemType
{
    Oil,
    Tire,
    Hammer,
    Wrench
}
