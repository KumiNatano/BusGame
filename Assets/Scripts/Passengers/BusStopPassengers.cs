using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BusStopPassengers : MonoBehaviour
{
    public List<Passenger> Passengers;
    private bool arePassengersSelected = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BusDoor") && other.GetComponentInParent<BusSystems>().areDoorsOpened == true)
        {
            //PassengersGettingOnTheBus(other.GetComponentInParent<BusPassengers>(), other.GetComponentInParent<Transform>());
            if (arePassengersSelected == false)
            {
                SelectPassangersToGo(other.GetComponentInParent<BusPassengers>());
                arePassengersSelected = true;
            }
            PassengersGettingOffTheBus(other.GetComponentInParent<BusPassengers>());
        }
    }

    private void PassengersGettingOnTheBus(BusPassengers busPassengers, Transform busTransform)
    {
        int busCapacity = busPassengers.GetActualCapacity();
        int numberOfPPlOnBusStop = Passengers.Count;

        
        for (int i = 0; i < Passengers.Count; i++)
        {
            if (numberOfPPlOnBusStop == 0 || busCapacity == 0)
            {
                break;
            }

            if (Passengers[i].Destination == name)
            {
                continue;
            }
            
            busCapacity--;
            numberOfPPlOnBusStop--;
            busPassengers.AddPassengerOnList(Passengers[i]);
            Passengers[i].transform.parent = busTransform;
            //Passengers[i].GoToDoor(busPassengers.DoorPositions);
            Passengers[i] = null;
        }
        
        Passengers.RemoveAll(p => p == null);
    }

    private void SelectPassangersToGo(BusPassengers busPassengers)
    {
        int busCapacity = busPassengers.GetActualCapacity();
        int numberOfPPlOnBusStop = Passengers.Count;

        for (int i = 0; i < Passengers.Count; i++)
        {
            if (numberOfPPlOnBusStop == 0 || busCapacity == 0)
            {
                break;
            }

            if (Passengers[i].Destination == name)
            {
                continue;
            }

            busCapacity--;
            numberOfPPlOnBusStop--;
            Passengers[i].isGoingToDoor = true;
        }
        
        
    }

    private void AssignToBus(BusPassengers busPassengers, Transform busTransform)
    {
        
    }

    private void PassengersGettingOffTheBus(BusPassengers busPassengers)
    {
        for (int i = 0; i < busPassengers.Passengers.Count; i++)
        {
            if (busPassengers.Passengers[i].Destination == this.name)
            {
                busPassengers.Passengers[i].transform.parent = transform;
                
            }
        }
    }
}
