using System;
using UnityEngine;

public class ResourceOwner : MonoBehaviour
{
    [SerializeField] private CollectionPlace _collectionPlace;

    private int _amountCollectedResources;
    private int _requiredAmountResources = 1;

    public event Action StatsChanged;
    public event Action RequiredAmountResourcesCollected;

    public CollectionPlace CollectionPlace => _collectionPlace;

    public int AmountCollectedResource => _amountCollectedResources;

    public void Initialize()
    {
        _amountCollectedResources = 0;
    }

    private void OnEnable()
    {
        _collectionPlace.ResourceReceived += IncreaseAmountCollectedResources;
    }

    private void OnDisable()
    {
        _collectionPlace.ResourceReceived -= IncreaseAmountCollectedResources;
    }

    public int GetAmountResources(int requiredAmountResources)
    {
        if (requiredAmountResources < 0 || _amountCollectedResources < requiredAmountResources)
            return 0;

        _amountCollectedResources -= requiredAmountResources;
        StatsChanged?.Invoke();

        return requiredAmountResources;
    }

    public void SetRequiredAmountResources(int requiredAmountResources)
    {
        if (requiredAmountResources > 0)
            _requiredAmountResources = requiredAmountResources;
    }

    private void IncreaseAmountCollectedResources()
    {
        _amountCollectedResources++;
        StatsChanged?.Invoke();

        if (_amountCollectedResources >= _requiredAmountResources)
            RequiredAmountResourcesCollected?.Invoke();
    }
}