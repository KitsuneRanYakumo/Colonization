using System;

public class StatsCollectorBase
{
    private CollectorBase _collectorBase;

    public StatsCollectorBase(CollectorBase collectorBase)
    {
        _collectorBase = collectorBase;
    }

    public event Action StatsChanged;

    public int TotalAmountUnits { get; private set; }

    public int FreeAmountUnits { get; private set; }

    public int BusyAmountUnits { get; private set; }

    public int DiscoveredAmountResources { get; private set; }

    public int CollectedAmountResource { get; private set; }

    public void Update()
    {
        TotalAmountUnits = _collectorBase.UnitOwner.TotalAmountUnits;
        FreeAmountUnits = _collectorBase.UnitOwner.FreeAmountUnits;
        BusyAmountUnits = _collectorBase.UnitOwner.BusyAmountUnits;
        DiscoveredAmountResources = _collectorBase.KeeperDiscoveredResources.AmountUntouchedResources;
        CollectedAmountResource = _collectorBase.ResourceOwner.AmountCollectedResource;
        StatsChanged?.Invoke();
    }
}