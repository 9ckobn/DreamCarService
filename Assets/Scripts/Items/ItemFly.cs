using UnityEngine;

public class ItemFly
{
    public Player _player;

    public GameObject currentItem;

    public void GetObject()
    {
        if (currentItem == null)
            return;

        if (currentItem.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        currentItem.transform.SetParent(_player.StackParent.gameObject.transform, true);

        if (currentItem.GetComponent<Item>().itemType == TypeConfigurator.ItemType.Wrench)
            if (_player.AllItems.Count == 0 || _player.AllItems[_player.AllItems.Count - 1].gameObject.transform.localEulerAngles.x == 9.999999f)

                currentItem.transform.localEulerAngles = new Vector3(-10, 0, 0);
            else
                currentItem.transform.localEulerAngles = new Vector3(10, 0, 0);
        else
            currentItem.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);

        if (_player.ItemSender == null)
            _player.ItemSender = _player.gameObject.AddComponent<ItemSender>();

        _player.ItemSender.GetObjectToHands(_player, currentItem);

        _player.StackPointer.transform.localPosition = SetNextPosition(true);

        _player.AllItems.Add(currentItem.GetComponent<Item>());

        _player.AnimatorController.isWithHands = _player.AllItems.Count > 0;

        EventListener.OnItemGet();
    }

    public Vector3 SetNextPosition(bool ifGet)
    {
        if (ifGet)
            return new Vector3(0, _player.StackPointer.transform.localPosition.y + currentItem.GetComponent<BoxCollider>().bounds.size.y + _player.ItemStackOffset, 0);
        else
            return new Vector3(0, _player.StackPointer.transform.localPosition.y - currentItem.GetComponent<BoxCollider>().bounds.size.y - _player.ItemStackOffset, 0);
    }

    public void SendObject(Vector3 placeToSend)
    {
        if (currentItem == null)
            return;


        var itemIndex = GetItemIndex();

        var bounds = _player.AllItems[itemIndex].GetComponent<BoxCollider>().bounds.size.y;

        for (int i = itemIndex; i < _player.AllItems.Count; i++)
            _player.AllItems[i].transform.localPosition -= new Vector3(0, bounds, 0);

        currentItem.transform.SetParent(null, true);

        if (_player.ItemSender == null)
            _player.ItemSender = _player.gameObject.AddComponent<ItemSender>();

        _player.ItemSender.SpendObject(_player, currentItem, placeToSend);

        _player.StackPointer.transform.localPosition = SetNextPosition(false);

        _player.AllItems.Remove(currentItem.GetComponent<Item>());

        _player.AnimatorController.isWithHands = _player.AllItems.Count > 0;

        EventListener.OnItemSend();
    }

    public int GetItemIndex()
    {
        for (int i = _player.AllItems.Count; i > 0; i--)
        {
            if (_player.AllItems[i - 1].itemType == currentItem.GetComponent<Item>().itemType)
            {
                return i - 1;
            }
        }
        return 0;
    }
}