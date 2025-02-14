using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : Spawnable<Resource>
{
    private Rigidbody _rigidbody;
    private bool _isTaken = false;

    public event Action<Resource> Taken;

    public bool IsTaken => _isTaken;

    public override void Reset()
    {
        _isTaken = false;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void BecomeTaken(Transform parent)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(parent);
        _isTaken = true;
        Taken?.Invoke(this);
    }

    public void BecomeReleased()
    {
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }
}