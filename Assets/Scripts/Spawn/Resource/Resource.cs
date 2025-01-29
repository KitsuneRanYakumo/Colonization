using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : Spawnable<Resource>
{
    private Rigidbody _rigidbody;

    public override void Reset()
    {
        _rigidbody.isKinematic = false;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void BecomeTaken()
    {
        _rigidbody.isKinematic = true;
    }
}