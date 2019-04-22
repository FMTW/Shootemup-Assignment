using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private bool spawnMode;

    private Vector3 spawnPosition;

    [Header("Temp")]
    public float speed;

    private void Update()
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        spawnPosition = transform.position + new Vector3(0, 10, 0);

    }

    private void OnBecameVisible()
    {
        if (spawnMode)
            Instantiate(spawnObjects[0], spawnPosition, transform.rotation);
    }

    private void OnBecameInvisible()
    {   
        if (!spawnMode)
            Instantiate(spawnObjects[0], spawnPosition, Quaternion.Euler(0,180,0));
    }
}
