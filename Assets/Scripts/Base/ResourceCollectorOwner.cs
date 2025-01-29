using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectorOwner : MonoBehaviour
{
    [SerializeField] private ResourceCollectorSpawner _collectorSpawner;
    [SerializeField] private PlaceSpawner _waitingPlaceSpawner;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<ResourceCollector> _freeCollectors;

    private CollectionPlace _collectionPlace;
    private int _totalAmount;
    private int _busyAmount;

    public event Action<int> TotalAmountCollectorsChanged;
    public event Action<int> FreeAmountCollectorsChanged;
    public event Action<int> BusyAmountCollectorsChanged;

    public int FreeAmountCollector => _freeCollectors.Count;

    public void Initialize(CollectionPlace collectionPlace)
    {
        _totalAmount = 0;
        _busyAmount = 0;

        TotalAmountCollectorsChanged?.Invoke(_totalAmount);
        FreeAmountCollectorsChanged?.Invoke(_freeCollectors.Count);
        BusyAmountCollectorsChanged?.Invoke(_busyAmount);

        _collectorSpawner.Initialize(_freeCollectors);
        _waitingPlaceSpawner.Initialize();
        _collectionPlace = collectionPlace;
        InitializeStartCollectors();
    }

    public void CreateResourceCollector()
    {
        ResourceCollector resourceCollector = _collectorSpawner.GetSpawnable();
        resourceCollector.transform.position = _spawnPoint.position;
        resourceCollector.Initialize(_waitingPlaceSpawner.GetSpawnable(), _collectionPlace);
        _freeCollectors.Add(resourceCollector);
        _totalAmount++;

        TotalAmountCollectorsChanged?.Invoke(_totalAmount);
        FreeAmountCollectorsChanged?.Invoke(_freeCollectors.Count);
    }

    public ResourceCollector GetFreeResourceCollector()
    {
        ResourceCollector collector = _freeCollectors[^1];
        return collector;
    }

    private void InitializeStartCollectors()
    {
        for (int i = 0; i < _freeCollectors.Count; i++)
        {
            _freeCollectors[i].Initialize(_waitingPlaceSpawner.GetSpawnable(), _collectionPlace);
            _freeCollectors[i].BecomeFree += AddFreeCollector;
            _freeCollectors[i].BecomeBusy += RemoveBusyCollector;
            _freeCollectors[i].LifeTimeFinished += RemoveAllEventHandlers;
            _totalAmount++;
            TotalAmountCollectorsChanged?.Invoke(_totalAmount);
        }
    }

    private void AddFreeCollector(ResourceCollector collector)
    {
        _freeCollectors.Add(collector);
        _busyAmount--;

        FreeAmountCollectorsChanged?.Invoke(_freeCollectors.Count);
        BusyAmountCollectorsChanged?.Invoke(_busyAmount);
    }

    private void RemoveBusyCollector(ResourceCollector collector)
    {
        _freeCollectors.Remove(collector);
        _busyAmount++;

        FreeAmountCollectorsChanged?.Invoke(_freeCollectors.Count);
        BusyAmountCollectorsChanged?.Invoke(_busyAmount);
    }

    private void RemoveAllEventHandlers(ResourceCollector collector)
    {
        collector.BecomeFree -= AddFreeCollector;
        collector.BecomeBusy -= RemoveBusyCollector;
        collector.LifeTimeFinished -= RemoveAllEventHandlers;
    }
}