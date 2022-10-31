using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public MoneyCounter moneyCounter;

    void Start()
    {
        moneyCounter.MoneyCount = Game.playerUpgradesData.MoneyCount;
    }

    void OnDestroy()
    {
        Game.playerUpgradesData.MoneyCount = moneyCounter.MoneyCount;
    }
}
