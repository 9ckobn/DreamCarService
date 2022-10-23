using UnityEngine;
public class Game
{
    public static IInputService InputService;

    public static ItemPrefabData itemData;

    private const string itemDataPath = "";

    public Game()
    {
        RegisterInputService();
    }

    private static void RegisterInputService()
    {
        if (Application.isEditor)
            InputService = new StandaloneInputModule();
        else
            InputService = new MobileInputModule();
    }

    private static void RegisterDataResources()
    {
        //itemData = Resources.Load();
    }
}