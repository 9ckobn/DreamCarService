using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private PlayerConfigurator playerConfig;
    private IInputService _inputSrevice;
    private Camera _camera;
    private CharacterController CharacterController;

    [SerializeField] private float Speed;

    void Awake()
    {
        _inputSrevice = Game.InputService;

        playerConfig = new PlayerConfigurator
        {
            _speed = Speed,
            _characterController = GetComponent<CharacterController>(),
            _animator = GetComponent<Animator>()
        };
    }

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputSrevice.Axis.sqrMagnitude > float.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(_inputSrevice.Axis);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
        }

        movementVector += Physics.gravity;

        playerConfig._characterController.Move(playerConfig._speed * movementVector * Time.deltaTime);
    }
}
