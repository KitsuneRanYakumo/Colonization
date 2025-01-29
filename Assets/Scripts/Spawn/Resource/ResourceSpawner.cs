using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private CircularSpawnGrid _circularSpawnGrid;
    [SerializeField] private int _amountCircles;
    [SerializeField] private float _spawnDelay = 1;

    public override void Initialize(List<Resource> resources = null)
    {
        base.Initialize(resources);
        _circularSpawnGrid.Initialize(_amountCircles);
    }

    public void StartSpawnResource()
    {
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            GetSpawnable().Initialize(_circularSpawnGrid.GetRandomPosition());
            yield return wait;
        }
    }
}