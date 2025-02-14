using UnityEngine;

public class ViewStatsSelectedCollectorBase : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private ViewAmount _viewTotalAmountUnits;
    [SerializeField] private ViewAmount _viewFreeAmountUnits;
    [SerializeField] private ViewAmount _viewBusyAmountUnits;
    [SerializeField] private ViewAmount _viewAmountDiscoveredResource;
    [SerializeField] private ViewAmount _viewAmountCollectedResource;
    [SerializeField] private ViewCooldown _viewCooldown;

    private StatsCollectorBase _statsCollectorBase;

    private void OnEnable()
    {
        _game.AnotherConstructionSelected += SubscribeCollectorBase;
    }

    private void OnDisable()
    {
        _game.AnotherConstructionSelected -= SubscribeCollectorBase;
    }

    private void SubscribeCollectorBase(CollectorBase selectedCollectorBase)
    {
        if (_statsCollectorBase != null)
        {
            _statsCollectorBase.StatsChanged -= UpdateStats;
        }

        _statsCollectorBase = selectedCollectorBase.Stats;
        _viewCooldown.SetTimer(selectedCollectorBase.Timer);
        _statsCollectorBase.StatsChanged += UpdateStats;
        UpdateStats();
    }

    private void UpdateStats()
    {
        _viewTotalAmountUnits.DisplayAmount(_statsCollectorBase.TotalAmountUnits);
        _viewFreeAmountUnits.DisplayAmount(_statsCollectorBase.FreeAmountUnits);
        _viewBusyAmountUnits.DisplayAmount(_statsCollectorBase.BusyAmountUnits);
        _viewAmountDiscoveredResource.DisplayAmount(_statsCollectorBase.DiscoveredAmountResources);
        _viewAmountCollectedResource.DisplayAmount(_statsCollectorBase.CollectedAmountResource);
    }
}