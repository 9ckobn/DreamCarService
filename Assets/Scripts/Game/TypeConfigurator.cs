using UnityEngine;

public static class TypeConfigurator
{
    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Tire:
                return Game.wishSpritesData.SpriteArray[0];
            case ItemType.Oil:
                return Game.wishSpritesData.SpriteArray[1];
            case ItemType.Wrench:
                return Game.wishSpritesData.SpriteArray[2];
            case ItemType.Accumulator:
                return Game.wishSpritesData.SpriteArray[3];
            default: return null;
        }
    }

    public static int AllowedCountOfItems(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Tire:
                return Game.playerUpgradesData.TireCount;
            case ItemType.Oil:
                return Game.playerUpgradesData.OilCount;
            default:
                return 1;
        }
    }


    public enum ItemType
    {
        Oil,
        Tire,
        Wrench,
        Accumulator
    }
}
