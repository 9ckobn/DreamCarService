using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

    public class ItemFly
    {
        public Player _player;

        public GameObject currentItem;

        public void GetObject()
        {
            if(_player.lastItemPosition == Vector3.zero)
                currentItem.transform.position = _player.StackPosition.position;
            else
                currentItem.transform.position = _player.lastItemPosition + new Vector3(0, currentItem.GetComponent<BoxCollider>().bounds.size.y + _player.itemOffset, 0);

            currentItem.transform.parent = _player.StackPosition.gameObject.transform;
            _player.lastItemPosition = currentItem.transform.position;
        }

        public void SendObject(Vector3 placeToSend)
        {
            //currentItem = _player.currentItemsArray[_player.currentItemsArray.Count - 1]
        }
    }
