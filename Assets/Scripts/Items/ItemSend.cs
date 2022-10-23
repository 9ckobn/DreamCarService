using UnityEngine;

public class ItemSend : MonoBehaviour
{
    [SerializeField] ItemType itemType;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();

            foreach (var item in player.playerCurrentItems)
            {
                if(item.itemType == itemType)
                {
                    
                }
            }
        }
    }
}

