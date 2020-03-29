﻿using System.Collections;
using System.Collections.Generic;
using GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    enum DestinationType 
    {
        ClosestDestination,
        RandomDestination,
    }
    [SerializeField] private NavMeshAgent movementAgent;
    [SerializeField] private List<UnitType> destinationTypes;
    [SerializeField] private float RateOfTargetSearchInSec = 1;
    [SerializeField] private float searchRange = 5.0f;
    [SerializeField] private float destinationSphereVolumeRadious = 0.5f;
    [SerializeField] private TextMeshProUGUI textStatus;
    [SerializeField] private string status = "Disabled";
    // [SerializeField] private DestinationType destinationType = DestinationType.RandomDestination;

    List<GameObject> destinationTransform;
    int destPointIndex = 0;
    bool destinationFound = false;
    string destinationName = "";

    private void Start()
    {
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        movementAgent.autoBraking = false;

        destinationTransform = new List<GameObject>();
    }

    private float nextBeatTime = 0.0f;
    public float periodTempo = 0.1f;

    private void Update()
    {
        if (Time.time > nextBeatTime ) 
        {
            nextBeatTime += periodTempo;
            // execute block of code here
            // if no destination start searching for new target destination
            SearchForTarget(RateOfTargetSearchInSec);
        }
        else
        {
            // wait for periodTempo sec till next beat
        }

        status = " ";

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (destinationFound && !movementAgent.pathPending && movementAgent.remainingDistance < destinationSphereVolumeRadious)
            MoveToNextTarget();
    }

    private void SearchForTarget(float sec)
    {
        Debug.Log("Agent Searching");
        Vector3 searchCenter = transform.position;
        destinationTransform.Clear();
        destinationFound = false;
        GameObject newDestinationObject;
        // while(!destinationFound)
        // {

            Collider[] hitColliders = Physics.OverlapSphere(searchCenter, searchRange);
            int i = 0;
            while (i < hitColliders.Length)
            {
                newDestinationObject = hitColliders[i].gameObject;
                // if search sphere collided with agent
                if(newDestinationObject.GetComponent<Agents>() != null)
                {

                    //Check if agent is part of destinationTarget
                    foreach (UnitType destination in destinationTypes)
                    {
                        if(newDestinationObject.GetComponent<Agents>().unitType == destination)
                        {
                            destinationTransform.Add(newDestinationObject);
                            destinationFound = true;
                        }
                    }
                }
                i++;
            }
        // }
        // yield return new WaitForSeconds(sec);
    }

    private void MoveToNextTarget()
    {
        // Returns if no points have been set up
        if (destinationTransform.Count == 0 || destPointIndex < 0 || destPointIndex >= destinationTransform.Count)
        {
            status = " I am Lost ";
            return;
        }

        // Update status
        status = " I am moving to " + destinationTransform[destPointIndex].GetComponent<Agents>().GetAgentName();

        // Set the agent to go to the currently selected destination.
        movementAgent.destination = destinationTransform[destPointIndex].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPointIndex = (destPointIndex + 1) % destinationTransform.Count;
 
    }

}
