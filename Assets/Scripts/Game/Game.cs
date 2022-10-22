using UnityEngine;
public class Game
{
    public static IInputService InputService;

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
}