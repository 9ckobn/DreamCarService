using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    private Game _game;

    void Awake()
    {
        _game = new Game();

        DontDestroyOnLoad(gameObject);
    }
}
