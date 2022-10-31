using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    private Game _game;

    void Awake()
    {
        _game = new Game();

        DontDestroyOnLoad(gameObject);
    }


    void OnApplicationQuit()
    {
        WritePlayerData.WriteObject(Game.playerUpgradesData, "Upgrades.json");
    }


}
