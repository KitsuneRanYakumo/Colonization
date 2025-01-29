using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CollectorBase _collectorBase;
    [SerializeField] private List<ResourceSpawner> _resourceSpawners;

    private void Start()
    {
        _collectorBase.Initialize();
        _collectorBase.StartCollectResources();

        for (int i = 0;  i < _resourceSpawners.Count; i++)
        {
            _resourceSpawners[i].Initialize();
            _resourceSpawners[i].StartSpawnResource();
        }
    }
}