using System;
using UnityEngine;

[RequireComponent(typeof(ResourceDetector))]
public class CollectionPlace : MonoBehaviour
{
    private ResourceDetector _resourceDetector;

    public event Action ResourceReceived;

    private void Awake()
    {
        _resourceDetector = GetComponent<ResourceDetector>();
    }

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