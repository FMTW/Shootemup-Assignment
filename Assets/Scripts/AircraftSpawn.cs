using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSpawn : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject airCraft;
    [SerializeField] private GameObject clone;
    [SerializeField] private int spawnCount;

    private void OnBecameVisible()
    {
        StartCoroutine(SpawnUAOs());
    }

    private void OnBecameInvisible()
    {
        Destroy(clone, 2f);
    }

    IEnumerator SpawnUAOs()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            clone = Instantiate(airCraft, spawnPosition.position, transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Aircrafts").transform;
            yield return new WaitForSeconds(0.8f);
        }
    }
}
