using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private PlayerConfigurator playerConfig;
    private IInputService _inputSrevice;
    private Camera _camera;
    private CharacterController CharacterController;
    private PlayerAnimatorController AnimatorController;

    [SerializeField] private float Speed;

    [Header("Effects")]
    [Space(10)]
    [SerializeField] private ParticleSystem footParticles;

    void Awake()
    {
        _inputSrevice = Game.InputService;

        playerConfig = new PlayerConfigurator
        {
            _speed = Speed,
            _characterController = GetComponent<CharacterController>(),
            _animator = GetComponent<Animator>()
        };

        AnimatorController = gameObject.AddComponent<PlayerAnimatorController>();
        AnimatorController.animator = playerConfig._animator;
        AnimatorController.characterController = playerConfig._characterController;
    }

    void Start()
    {
        _camera = Camera.main;
    }

    [System.Obsolete]
    void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputSrevice.Axis.sqrMagnitude > float.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(_inputSrevice.Axis);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;

            footParticles.enableEmission = true;
        }
        else
        {
            footParticles.enableEmission = false;
        }

        movementVector += Physics.gravity;

        playerConfig._characterController.Move(playerConfig._speed * movementVector * Time.deltaTime);
    }

}