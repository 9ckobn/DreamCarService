using System.IO;
using Newtonsoft.Json;
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
        var writeData = new WritePlayerData(Game.playerUpgradesData);
    }


}
