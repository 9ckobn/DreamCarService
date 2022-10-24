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
                return 8;
            default:
                return 0;
        }
    }
}
