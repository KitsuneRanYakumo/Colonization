using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private CircularGrid _circularSpawnGrid;
    [SerializeField] private int _amountCircles;
    [SerializeField] private float _spawnDelay = 1;
    [SerializeField] private float _resourceDetectorSphereSize = 1;

    public override void Initialize(List<Resource> resources = null)
    {
        base.Initialize(resources);
        _circularSpawnGrid.Initialize(transform.eulerAngles.y, _amountCircles);
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
            Vector3 possibleSpawnPosition = _circularSpawnGrid.GetRandomPosition();
            Collider[] colliders = Physics.OverlapSphere(possibleSpawnPosition, _resourceDetectorSphereSize);
            bool canSpawn = true;

            foreach (var collider in colliders)
            {
                if (collider.GetComponent<Resource>() != null)
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                GetSpawnable().Initialize(possibleSpawnPosition);
            }

            yield return wait;
        }
    }
}