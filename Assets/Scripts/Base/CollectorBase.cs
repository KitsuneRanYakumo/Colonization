using System.Collections;
using UnityEngine;

public class CollectorBase : MonoBehaviour
{
    [SerializeField] private ResourceCollectorOwner _resourceCollectorOwner;
    [SerializeField] private ResourceOwner _resourceOwner;

    public void Initialize()
    {
        _resourceCollectorOwner.Initialize(_resourceOwner.CollectionPlace);
        _resourceOwner.Initialize();
    }

    public void StartCollectResources()
    {
        StartCoroutine(CollectResources());
    }

    private IEnumerator CollectResources()
    {
        while (enabled)
        {
            if (_resourceCollectorOwner.FreeAmountCollector > 0)
            {
                if (_resourceOwner.UncollectedResources > 0)
                {
                    _resourceCollectorOwner.GetFreeResourceCollector().GoToResource(_resourceOwner.GetDiscoveredResource());
                }
            }
            yield return null;
        }
    }
}