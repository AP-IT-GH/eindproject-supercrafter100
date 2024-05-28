using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitlauncher : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 5f;
    public float minLaunchInterval = 1f;
    public float maxLaunchInterval = 3f;
    public float minLaunchAngle = 30f;
    public float maxLaunchAngle = 60f;
    public float minUpwardForce = 15f;
    public float maxUpwardForce = 25f;

    void Start()
    {
        StartCoroutine(ContinuousFruitLaunch());
    }

    IEnumerator ContinuousFruitLaunch()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minLaunchInterval, maxLaunchInterval));
            LaunchFruit();
        }
    }

    public void LaunchFruit()
    {
        GameObject randomFruitPrefab = fruitPrefabs[UnityEngine.Random.Range(0, fruitPrefabs.Length)];
        GameObject fruitInstance = Instantiate(randomFruitPrefab, launchPoint.position, Quaternion.identity);

        float launchAngle = UnityEngine.Random.Range(minLaunchAngle, maxLaunchAngle);
        float upwardForce = UnityEngine.Random.Range(minUpwardForce, maxUpwardForce);

        // Bereken de initi�le lanceerrichting van het fruit met de hoek
        Vector3 launchDirection = Quaternion.Euler(0f, launchAngle, 0f) * launchPoint.forward;

        // Verminder de schuinte van de lanceerrichting
        launchDirection = Vector3.Lerp(launchDirection.normalized, Vector3.up, 0.8f);

        Rigidbody fruitRigidbody = fruitInstance.GetComponent<Rigidbody>();
        fruitRigidbody.AddForce(launchDirection * upwardForce, ForceMode.Impulse);

        float fallSpeed = UnityEngine.Random.Range(minFallSpeed, maxFallSpeed);
        fruitRigidbody.velocity = launchDirection * -fallSpeed;

        fruitInstance.AddComponent<FruitDestroyer>();
    }
}

public class FruitDestroyer : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedDirection = Vector3.Reflect(rb.velocity.normalized, normal);

            Vector3 newDirection = Vector3.Lerp(reflectedDirection, Vector3.up, 0.3f).normalized;
            rb.velocity = newDirection * rb.velocity.magnitude;
            rb.AddForce(newDirection * 5f, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}