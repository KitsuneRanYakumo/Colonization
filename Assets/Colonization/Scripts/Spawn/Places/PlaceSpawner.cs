using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpawner : Spawner<WaitingPlace>
{
    [SerializeField] private CircularGrid _spawnGrid;
    
    public override void Initialize(List<WaitingPlace> places = null)
    {
        _spawnGrid.Initialize(transform.eulerAngles.y);
        base.Initialize(places);
    }

    protected override IEnumerator CreateSpawnables(int amountSpawnables)
    {
        for (int i = 0; i < amountSpawnables; i++)
        {
            CreateSpawnable();

            yield return null;
        }
    }

    protected override void CreateSpawnable()
    {
        WaitingPlace place = Instantiate(PrefabSpawnable);
        place.Initialize(_spawnGrid.GetCurrentPosition());
        place.Off();
        Spawnables.Add(place);
    }
}