using System;
using static TypeConfigurator;

public class ClientWishes
{
    public ItemType wishType;

    public ClientWishes(WishHandler wishHandler)
    {
        var typeArray = Enum.GetValues(typeof(ItemType));
        Random random = new Random();
        ItemType randomType = (ItemType)typeArray.GetValue(random.Next(typeArray.Length));

        wishType = randomType;

        var itemSender = wishHandler.InitSpender();
        itemSender.itemType = wishType;

        wishHandler.wishType = wishType;
    }


}
