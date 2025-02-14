using System;
using System.Collections.Generic;
using UnityEngine;

public class KeeperDiscoveredResources : MonoBehaviour
{
    [SerializeField] private TerritoryScanner _territoryScanner;

    private List<Resource> _resources;
    private int _amountUntouchedResources;

    public event Action StatsChanged;

    public Timer Timer => _territoryScanner.Timer;

    public int AmountUntouchedResources => _amountUntouchedResources;

    public int AmountDiscoveredResources => _resources.Count;

    public void Initialize()
    {
        _resources = new List<Resource>();
        _amountUntouchedResources = 0;
        _territoryScanner.Initialize();
        _territoryScanner.Scan();
    }

    private void OnEnable()
    {
        _territoryScanner.ResourceFound += AddResource;
        _territoryScanner.CooldownEnded += _territoryScanner.Scan;
    }

    private void OnDisable()
    {
        _territoryScanner.ResourceFound -= AddResource;
        _territoryScanner.CooldownEnded -= _territoryScanner.Scan;
    }

    public Resource GetDiscoveredResource()
    {
        if (_resources.Count == 0)
            return null;

        int resourceIndex = 0;
        Resource resource = _resources[resourceIndex];
        _resources.RemoveAt(resourceIndex);
        resource.Taken += DecreaseAmountUntouchedResources;

        return resource;
    }

    private void AddResource(Resource resource)
    {
        if (_resources.Contains(resource))
            return;

        if (resource.IsTaken)
            return;

        SubscribeResource(resource);
        _resources.Add(resource);
        _amountUntouchedResources++;
        StatsChanged?.Invoke();
    }

    private void DecreaseAmountUntouchedResources(Resource resource)
    {
        _amountUntouchedResources--;
        StatsChanged?.Invoke();
        resource.Taken -= DecreaseAmountUntouchedResources;
    }

    private void SubscribeResource(Resource resource)
    {
        resource.LifeTimeFinished += UnsubscribeResource;
    }

    private void UnsubscribeResource(Resource resource)
    {
        resource.LifeTimeFinished -= UnsubscribeResource;
    }
}