using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SpotsHandler : MonoBehaviour
{
    public List<Spot> AllSpots;

    void Start()
    {
        AllSpots = GetComponentsInChildren<Spot>().ToList();
    }
}
