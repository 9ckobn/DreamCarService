using UnityEngine;

public class ItemFly
{
    public Player _player;

    public GameObject currentItem;

    public void GetObject()
    {
        if (currentItem == null)
            return;

        currentItem.transform.SetParent(_player.StackParent.gameObject.transform, true);

        currentItem.transform.localEulerAngles = Vector3.zero;

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
            return new Vector3(0, _player.StackPointer.transform.localPosition.y + currentItem.GetComponent<BoxCollider>().bounds.size.y + _player.ParabolaHeight, 0);
        else
            return new Vector3(0, _player.StackPointer.transform.localPosition.y - currentItem.GetComponent<BoxCollider>().bounds.size.y - _player.ParabolaHeight, 0);
    }

    public void SendObject(Vector3 placeToSend)
    {
        if (currentItem == null)
            return;

        currentItem.transform.SetParent(null, true);

        if (_player.ItemSender == null)
            _player.ItemSender = _player.gameObject.AddComponent<ItemSender>();

        _player.ItemSender.SpendObject(_player, currentItem, placeToSend);

        _player.StackPointer.transform.localPosition = SetNextPosition(false);

        _player.AllItems.Remove(currentItem.GetComponent<Item>());

        _player.AnimatorController.isWithHands = _player.AllItems.Count > 0;

        EventListener.OnItemSend();
        //currentItem = _player.currentItemsArray[_player.currentItemsArray.Count - 1]
    }
}
