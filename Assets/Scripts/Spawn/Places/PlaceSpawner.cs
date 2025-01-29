using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpawner : Spawner<Place>
{
    [SerializeField] private CircularSpawnGrid _spawnGrid;
    
    public override void Initialize(List<Place> places = null)
    {
        _spawnGrid.Initialize();
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
        Place place = Instantiate(PrefabSpawnable);
        place.Initialize(_spawnGrid.GetCurrentPosition());
        place.Off();
        Spawnables.Add(place);
    }
}