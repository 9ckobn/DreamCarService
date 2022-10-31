using System.Collections;
using UnityEngine;

public interface IBuyable
{
    public void OnTriggerEnter(Collider other);

    public IEnumerator BuyProcess();

    public void GetItem(BuyableObject buyableObject);

    public void OnTriggerExit(Collider other);
}
