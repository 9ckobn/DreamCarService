using UnityEngine;

public class MoneyInstantiating : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _velocity = Vector3.up;
    private Vector3 _startPosition;
    
    private void Awake()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        this.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        _startPosition = transform.position;
        _velocity *= Random.Range(6f, 8f);
        _velocity += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
    }

    private void OnDisable()
    {
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        _rigidbody.position += _velocity * Time.deltaTime;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(Random.Range(-150f, 150f), Random.Range(150f, 250f), Random.Range(-150f, 150f)) * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

        if (_velocity.y < -8f) _velocity.y = -8f;
        else _velocity -= Vector3.up * 8 * Time.deltaTime;

        if (Mathf.Abs(_rigidbody.position.y - _startPosition.y) < 0.25f && _velocity.y < 0f)
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _velocity;
            this.enabled = false;
        }

    }
}
