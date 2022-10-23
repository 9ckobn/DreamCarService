using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerConfigurator
{
    public float _speed { get; set; }

    public Animator _animator { get; set; }

    public CharacterController _characterController { get; set; }

    public int AllowedCountOfItems(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Tire:
                return 4;
            default:
                return 0;
        }
    }
}
