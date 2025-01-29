using UnityEngine;

public class ViewInfoResourceCollectorOwner : MonoBehaviour
{
    [SerializeField] private ResourceCollectorOwner _resourceCollectorOwner;
    [SerializeField] private ViewAmount _viewTotalAmount;
    [SerializeField] private ViewAmount _viewFreeAmount;
    [SerializeField] private ViewAmount _viewBusyAmount;

    private void OnEnable()
    {
        _resourceCollectorOwner.TotalAmountCollectorsChanged += _viewTotalAmount.DisplayAmount;
        _resourceCollectorOwner.FreeAmountCollectorsChanged += _viewFreeAmount.DisplayAmount;
        _resourceCollectorOwner.BusyAmountCollectorsChanged += _viewBusyAmount.DisplayAmount;
    }

    private void OnDisable()
    {
        _resourceCollectorOwner.TotalAmountCollectorsChanged -= _viewTotalAmount.DisplayAmount;
        _resourceCollectorOwner.FreeAmountCollectorsChanged -= _viewFreeAmount.DisplayAmount;
        _resourceCollectorOwner.BusyAmountCollectorsChanged -= _viewBusyAmount.DisplayAmount;
    }
}