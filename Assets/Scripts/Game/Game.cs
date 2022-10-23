using UnityEngine;
public class Game
{
    public static IInputService InputService;

    public static ItemPrefabData itemData;

    public Game(ItemPrefabData _itemData)
    {
        RegisterInputService();
        itemData = _itemData;
    }

    private static void RegisterInputService()
    {
        if (Application.isEditor)
            InputService = new StandaloneInputModule();
        else
            InputService = new MobileInputModule();
    }

}