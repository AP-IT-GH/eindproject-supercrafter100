using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitlauncher : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Array van fruit prefab objecten
    public Transform launchPoint; // Het punt waar het fruit wordt afgeschoten
    public float launchForce = 10f; // Kracht waarmee het fruit wordt afgeschoten
    public float minFallSpeed = 2f; // Minimale snelheid waarmee het fruit naar beneden valt
    public float maxFallSpeed = 5f; // Maximale snelheid waarmee het fruit naar beneden valt
    public float minLaunchInterval = 1f; // Minimale interval tussen het afschieten van fruit
    public float maxLaunchInterval = 3f; // Maximale interval tussen het afschieten van fruit
    public float minLaunchAngle = 30f; // Minimale lanceerhoek (in graden)
    public float maxLaunchAngle = 60f; // Maximale lanceerhoek (in graden)
    public float minUpwardForce = 15f; // Minimale opwaartse kracht
    public float maxUpwardForce = 25f;
    void Start()
    {
        // Start de coroutine om fruit af te schieten
        StartCoroutine(ContinuousFruitLaunch());
    }

    // Coroutine om fruit continu af te schieten
    IEnumerator ContinuousFruitLaunch()
    {
        while (true)
        {
            // Wacht een willekeurige tijd tussen minLaunchInterval en maxLaunchInterval
            yield return new WaitForSeconds(UnityEngine.Random.Range(minLaunchInterval, maxLaunchInterval));

            // Roep de LaunchFruit-functie aan om fruit af te schieten
            LaunchFruit();
        }
    }

    // Functie om fruit af te schieten
    public void LaunchFruit()
    {
        // Kies willekeurig een fruit uit de prefab array
        GameObject randomFruitPrefab = fruitPrefabs[UnityEngine.Random.Range(0, fruitPrefabs.Length)];

        // Maak een instantie van het gekozen fruit prefab
        GameObject fruitInstance = Instantiate(randomFruitPrefab, launchPoint.position, Quaternion.identity);

        float launchAngle = UnityEngine.Random.Range(minLaunchAngle, maxLaunchAngle);
        float upwardForce = UnityEngine.Random.Range(minUpwardForce, maxUpwardForce);

        // Bereken de richting van de afschieting met de hoek en kracht
        Vector3 launchDirection = (launchPoint.up + Vector3.forward).normalized;
        launchDirection = Quaternion.Euler(0f, launchAngle, 0f) * launchDirection;

        // Geef het fruit kracht met de berekende richting en kracht
        Rigidbody fruitRigidbody = fruitInstance.GetComponent<Rigidbody>();
        fruitRigidbody.AddForce(launchDirection * upwardForce, ForceMode.Impulse);

        // Stel de val snelheid van het fruit in
        float fallSpeed = UnityEngine.Random.Range(minFallSpeed, maxFallSpeed);
        fruitRigidbody.velocity = launchDirection * -fallSpeed; // Aangepast: negatieve val snelheid

        // Voeg een script toe om het fruit te vernietigen wanneer het de grond raakt
        fruitInstance.AddComponent<FruitDestroyer>();
    }
}

// Script om het fruit te vernietigen wanneer het de grond raakt
public class FruitDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}