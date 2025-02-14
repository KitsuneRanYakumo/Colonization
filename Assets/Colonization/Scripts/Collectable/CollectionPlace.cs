using System;
using UnityEngine;

public class CollectionPlace : MonoBehaviour
{
    [SerializeField] private ResourceDetector _resourceDetector;

    public event Action ResourceReceived;

    private void OnEnable()
    {
        _resourceDetector.ResourceDetected += TakeResource;
    }

    private void OnDisable()
    {
        _resourceDetector.ResourceDetected -= TakeResource;
    }

    private void TakeResource(Resource resource)
    {
        ResourceReceived?.Invoke();
        resource.OnLifeTimeFinished(resource);
    }
}