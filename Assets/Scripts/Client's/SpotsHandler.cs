using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotsHandler : MonoBehaviour
{
    public Spot[] AllSpots;

    void Start()
    {
        AllSpots = GetComponentsInChildren<Spot>();
    }

    public Spot GetAvailableSpot()
    {
        foreach (var item in AllSpots)
            if (item.isAvailable)
                return item;

        Debug.Log("Here is not available spots, you need wait");
        return null;
    }

    public void BlockSpot(Spot spotToBlock) => spotToBlock.isAvailable = false;

}
