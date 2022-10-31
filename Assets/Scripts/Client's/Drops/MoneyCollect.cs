using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollect : MonoBehaviour, ICollectable
{
    Vector3 targetDirection;
    private bool hasTarget;
    private Rigidbody _rigidbody;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
    }


    void FixedUpdate()
    {
        if (hasTarget)
        {
            _rigidbody.velocity = targetDirection * 17.5f;
        }
    }

    public void Collect()
    {
        GameDependencies.instance._statsHandler.moneyCounter.MoneyCount += Random.Range(10, 30);

        gameObject.SetActive(false);
    }

    public void SetTarget(Vector3 target)
    {
        GetComponent<MoneyInstantiating>().enabled = false;

        _rigidbody.velocity = Vector3.zero;

        hasTarget = true;

        targetDirection = (target - transform.position).normalized;
    }
}
