using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusPassengers : MonoBehaviour
{
    public int BusMaxCapacity = 20;
    
    public List<Passenger> Passengers;

    public List<GameObject> DoorPositions;

    public List<Vector3> SeatsPositions;

    public int GetActualCapacity()
    {
        return BusMaxCapacity - Passengers.Count;
    }

    public void AddPassengerOnList(Passenger passenger)
    {
        Passengers.Add(passenger);
    }

    public void DeletePassengerFromList(Passenger passenger)
    {
        Passengers.Remove(passenger);
    }
}
