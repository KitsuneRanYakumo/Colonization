using System.Collections.Generic;
using UnityEngine;

public class PlaceOwner : MonoBehaviour
{
    [SerializeField] private PlaceSpawner _waitingPlaceSpawner;

    public Queue<WaitingPlace> _freePlaces;

    public void Initialize()
    {
        _freePlaces = new Queue<WaitingPlace>();
        _waitingPlaceSpawner.Initialize();
    }

    public WaitingPlace GetFreePlace()
    {
        if (_freePlaces.Count > 0)
        {
            WaitingPlace place = _freePlaces.Dequeue();
            place.gameObject.SetActive(true);
            return place;
        }
        else
        {
            return _waitingPlaceSpawner.GetSpawnable();
        }
    }

    public void TakeFreePlace(WaitingPlace place)
    {
        _freePlaces.Enqueue(place);
        place.gameObject.SetActive(false);
    }
}