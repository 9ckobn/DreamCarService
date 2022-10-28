using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Stack Configurator")]
    [Space]
    public Transform StackParent;
    public Transform StackPointer;
    public float ParabolaHeight;

    [HideInInspector]
    public ItemSender ItemSender;


    [Space(5)]
    public List<Item> AllItems;
    public List<PlayerCurrentItems> currentItemsArray;
    public PlayerConfigurator playerConfig;

    private IInputService _inputSrevice;
    private Camera _camera;
    public PlayerAnimatorController AnimatorController;

    [SerializeField] private float Speed;

    [Min(0.35f)]
    [SerializeField] private int TimeToGetItemInMS;

    [Space(5)]
    [Header("Effects")]
    [Space]
    [SerializeField] private ParticleSystem footParticles;

    void Awake()
    {
        _inputSrevice = Game.InputService;

        playerConfig = new PlayerConfigurator
        {
            _speed = Speed,
            _characterController = GetComponent<CharacterController>(),
            _animator = GetComponent<Animator>(),
            _timeToGetItemInMS = TimeToGetItemInMS
        };

        AnimatorController = gameObject.AddComponent<PlayerAnimatorController>();
        AnimatorController.animator = playerConfig._animator;
        AnimatorController.characterController = playerConfig._characterController;

        if (currentItemsArray == null)
            currentItemsArray = new List<PlayerCurrentItems>();

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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {


        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        Debug.Log("Collided!");
        // // Calculate push direction from move direction,
        // // we only push objects to the sides never up and down
        // Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // // If you know how fast your character is trying to move,
        // // then you can also multiply the push velocity by that.

        // // Apply the push
        // playerConfig._characterController.Move(-pushDir);
    }
}
