using System.Collections;
using UnityEngine;

public class ItemSend : MonoBehaviour
{
    [SerializeField] ItemType itemType;

    IEnumerator Send;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();

            foreach (var item in player.currentItemsArray)
            {
                if (item.itemType == itemType)
                {
                    Send = ItemSender(item);
                    StartCoroutine(Send);
                    break;
                }
            }
        }
    }

    void OnTriggerExit() => StopCoroutine(Send);

    IEnumerator ItemSender(PlayerCurrentItems playerCurrentItems)
    {
        yield return new WaitForSeconds(3);
        playerCurrentItems.currentCountOfItems--;
        Debug.Log(itemType + " was sended");

        yield break;
    }
}

