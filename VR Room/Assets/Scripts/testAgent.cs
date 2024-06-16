using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.GraphicsBuffer;

public class testAgent : Agent
{
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5;
    
    public Material rottenMaterial;
    public Material rottenShinyMaterial;
    public override void OnEpisodeBegin()
    {
        // reset de positie en orientatie als de agent gevallen is
        if (this.transform.localPosition.y < 0)
        {

            this.transform.localPosition = new Vector3(0, 0.5f, 0); this.transform.localRotation = Quaternion.identity;
        }

       /* // verplaats de target naar een nieuwe willekeurige locatie 
        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
        Target.localRotation = new Quaternion(0, Random.value * 360f, 0, 1);*/
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en�Agent posities
        GameObject closestFruit = GetClosestFruit();
        if (closestFruit == null) { return; }
        
        sensor.AddObservation(GetClosestFruit().transform.position);
        sensor.AddObservation(this.transform.position);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Acties, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        transform.Translate(controlSignal * speedMultiplier);
        transform.Rotate(0.0f, rotationMultiplier * actionBuffers.ContinuousActions[1], 0.0f);

        // Beloningen
        GameObject closestFruit = GetClosestFruit();
        if (closestFruit == null) { return; }
        
        float distanceToTarget = Vector3.Distance(this.transform.position, closestFruit.transform.position);

        // target bereikt
        if (distanceToTarget < 1.42f && !closestFruit.gameObject.tag.Contains("rotten"))
        {
            List<Material> materials = new();
            for (int i = 0; i < closestFruit.gameObject.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                materials.Add(closestFruit.gameObject.tag.Contains("shiny") ? rottenShinyMaterial : rottenMaterial);
            }
            closestFruit.gameObject.GetComponent<MeshRenderer>().materials = materials.ToArray();
            closestFruit.gameObject.tag = closestFruit.gameObject.tag.Contains("shiny") ? "rottenshiny" : "rotten";
            
            SetReward(1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private GameObject GetClosestFruit()
    {
        // Find closest game object that is in the fruit layer
        GameObject[] fruits = FindGameObjectsWithLayer(LayerMask.NameToLayer("Fruit"));
        if (fruits == null) { return null; }
        
        GameObject closestFruit = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject fruit in fruits)
        {
            if (fruit.tag.Contains("rotten")) continue;
            
            float distance = Vector3.Distance(fruit.transform.position, position);
            if (distance < closestDistance)
            {
                closestFruit = fruit;
                closestDistance = distance;
            }
        }

        return closestFruit;
    }
    
    private GameObject[] FindGameObjectsWithLayer (int layer) 
    {
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer) { goList.Add(goArray[i]); }
        }
        if (goList.Count == 0) { return null; }
        return goList.ToArray();
    }
}
