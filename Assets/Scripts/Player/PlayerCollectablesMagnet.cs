using UnityEngine;

public class PlayerCollectablesMagnet : MonoBehaviour
{
    [SerializeField] private Transform CollectorPosition;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.SetTarget(CollectorPosition.position);
        }
    }
}
