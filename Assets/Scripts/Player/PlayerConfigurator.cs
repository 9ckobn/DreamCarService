using UnityEngine;

public class PlayerConfigurator
{
    public PlayerConfigurator()
    {
        _speed = Game.playerUpgradesData.Speed;
        _timeToGetItemInMS = Game.playerUpgradesData.GettingItemSpeed;
    }

    public float _speed { get; set; }

    public int _timeToGetItemInMS { get; set; }

    public int _moneyCount { get; set; }

    public Animator _animator { get; set; }

    public CharacterController _characterController { get; set; }
}
