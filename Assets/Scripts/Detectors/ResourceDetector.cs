using System;
using UnityEngine;

public class ResourceDetector : MonoBehaviour
{
    public event Action<Resource> ResourceDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            ResourceDetected?.Invoke(resource);
        }
    }
}