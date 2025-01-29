using UnityEngine;

public class ViewInfoResourceOwner : MonoBehaviour
{
    [SerializeField] private ResourceOwner _resourceOwner;
    [SerializeField] private ViewAmount _viewDiscoveredAmount;
    [SerializeField] private ViewAmount _viewCollectedAmount;

    private void OnEnable()
    {
        _resourceOwner.AmountDiscoveredResourcesChanged += _viewDiscoveredAmount.DisplayAmount;
        _resourceOwner.AmountCollectedResourcesChanged += _viewCollectedAmount.DisplayAmount;
    }

    private void OnDisable()
    {
        _resourceOwner.AmountDiscoveredResourcesChanged -= _viewDiscoveredAmount.DisplayAmount;
        _resourceOwner.AmountCollectedResourcesChanged -= _viewCollectedAmount.DisplayAmount;
    }
}