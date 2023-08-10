using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Passenger : MonoBehaviour
{
    public BusPassengers busPassengers;
    public Transform busTransform;

    private NavMeshAgent agent;
    public Vector3 closestDoorPosition;
    
    public string Destination;
    public bool isGoingToDoor = false;
    
    
    public bool isGoingToEdge;
    
    public List<Transform> BusDoors;
    
    public List<Transform> StairsEdgesToGo;
    public Transform StairEdgeToGo;
    
    public List<Transform> PointsInsideBus;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isGoingToDoor == true)
        {
            float distance = 999;
        
            foreach (var VARIABLE in BusDoors)
            {
                if (Vector3.Distance(this.transform.position, VARIABLE.transform.position) < distance)
                {
                    var position = VARIABLE.transform.position;
                    distance = Vector3.Distance(this.transform.position, position);
                    closestDoorPosition = position;
                }
            }
            agent.destination = closestDoorPosition;
        }

        if (isGoingToEdge)
        {
            var step =  1 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.TransformPoint(StairEdgeToGo.localPosition), step);
            if (Vector3.Distance(transform.position, transform.parent.TransformPoint(StairEdgeToGo.localPosition)) < 0.1f)
            {
                isGoingToEdge = false;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtTheDoor"))
        {
            isGoingToDoor = false;
            agent.ResetPath();
            
            if (other.name == "DOOR1")
            {
                StairEdgeToGo = StairsEdgesToGo[0];
            }
            else if(other.name == "DOOR2")
            {
                StairEdgeToGo = StairsEdgesToGo[1];
            }
            
            busPassengers.AddPassengerOnList(this);
            this.transform.parent = busTransform;
            
            isGoingToEdge = true;
        }
    }
}
