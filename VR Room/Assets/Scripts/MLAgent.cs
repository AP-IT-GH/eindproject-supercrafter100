using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class MLAgent : Agent
{
    // Variables used for resetting the targets that have to be collected
    public float minXValue = -5;
    public float maxXValue = 5;
    public float minYValue = -5;
    public float maxYValue = 5;
    public float minZValue = 5;
    public float maxZValue = 5;
    
    public GameObject target;
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 1;
    
    public override void OnEpisodeBegin()
    {
        // Spawn the target in a random position between the boundaries
        target.transform.localPosition = new Vector3(Random.Range(minXValue, maxXValue), Random.Range(minYValue, maxYValue), Random.Range(minZValue, maxZValue));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Move agent in three directions
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.y = actionBuffers.ContinuousActions[1];
        transform.Translate(controlSignal * speedMultiplier);

        Vector3 rotateSignal = Vector3.zero;
        rotateSignal.y = actionBuffers.ContinuousActions[2];
        transform.Rotate(rotateSignal * rotationMultiplier);
        
        // Reward depending on the distance to the target
        float distanceToTarget = Vector3.Distance(transform.localPosition, target.transform.localPosition);
        AddReward(distanceToTarget * -0.001f);
        
        AddReward(-1f / MaxStep);
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
        continuousActionsOut[2] = Input.GetAxis("Mouse X");
        
        Debug.Log("0: " + continuousActionsOut[0]);
        Debug.Log("1: " + continuousActionsOut[1]);
        Debug.Log("2: " + continuousActionsOut[2]);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("target"))
        {
            SetReward(1.0f);
            EndEpisode();
        }
    }
}
