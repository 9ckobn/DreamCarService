using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [CreateAssetMenu(fileName = "Upgrades", menuName = "Dream Car Service/Player/Player Upgrades")]
public class PlayerUpgrades : ScriptableObject, IWrittable
{
    public float Speed;

    [Min(0.35f)]
    public int GettingItemSpeed;

    public int TireCount;
    public int OilCount;
    public int WrenchCount;
    public int AccumulatorsCount;
    public int DefaulCount;

}
