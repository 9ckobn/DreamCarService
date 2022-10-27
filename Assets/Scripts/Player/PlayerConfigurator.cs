using UnityEngine;

public class PlayerConfigurator
{
    public float _speed { get; set; }

    public int _timeToGetItemInMS { get; set; }

    public Animator _animator { get; set; }

    public CharacterController _characterController { get; set; }

    public int AllowedCountOfItems(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Tire:
                return 4;
            case ItemType.Oil:
                return 8;
            default:
                return 0;
        }
    }
}
