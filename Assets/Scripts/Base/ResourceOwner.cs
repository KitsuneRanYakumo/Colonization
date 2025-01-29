using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOwner : MonoBehaviour
{
    [SerializeField] private CollectionPlace _collectionPlace;
    [SerializeField] private TerritoryScanner _territoryScanner;

    private List<Resource> _discoveredResources;
    private int _amountUncollectedResources;
    private int _amountCollectedResources;

    public event Action<int> AmountDiscoveredResourcesChanged;
    public event Action<int> AmountCollectedResourcesChanged;

    public CollectionPlace CollectionPlace => _collectionPlace;

    public int UncollectedResources => _amountUncollectedResources;

    public void Initialize()
    {
        _amountCollectedResources = 0;
        _amountUncollectedResources = 0;

        AmountCollectedResourcesChanged?.Invoke(_amountCollectedResources);

        _discoveredResources = new List<Resource>();
        _territoryScanner.ScanTerritory();
    }

    private void OnEnable()
    {
        _territoryScanner.ScanCompleted += _territoryScanner.ScanTerritory;
        _territoryScanner.ResourceFound += AddResourceInCollected;
        _collectionPlace.ResourceReceived += IncreaseAmountCollectedResources;
    }

    private void OnDisable()
    {
        _territoryScanner.ScanCompleted -= _territoryScanner.ScanTerritory;
        _territoryScanner.ResourceFound -= AddResourceInCollected;
        _collectionPlace.ResourceReceived -= IncreaseAmountCollectedResources;
    }

    public Resource GetDiscoveredResource()
    {
        Resource resource = _discoveredResources[^1];
        _amountUncollectedResources--;
        return resource;
    }

    private void AddResourceInCollected(Resource resource)
    {
        if (_discoveredResources.Contains(resource) == false)
        {
            resource.LifeTimeFinished += RemoveDiscoveredResource;
            _discoveredResources.Add(resource);
            _amountUncollectedResources++;
            AmountDiscoveredResourcesChanged?.Invoke(_discoveredResources.Count);
        }
    }

    private void RemoveDiscoveredResource(Resource resource)
    {
        resource.LifeTimeFinished -= RemoveDiscoveredResource;
        _discoveredResources.Remove(resource);
        AmountDiscoveredResourcesChanged?.Invoke(_discoveredResources.Count);
    }

    private void IncreaseAmountCollectedResources()
    {
        _amountCollectedResources++;
        AmountCollectedResourcesChanged?.Invoke(_amountCollectedResources);
    }
}