using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    public ItemPrefabData itemPrefabData;

    private Game _game;

    void Awake()
    {
        _game = new Game(itemPrefabData);

        DontDestroyOnLoad(gameObject);
    }
}
