using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSpawn : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private GameObject aircraft;
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
            clone = Instantiate(aircraft, new Vector3(transform.position.x, 10f, transform.position.z), transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Aircrafts").transform;
            yield return new WaitForSeconds(0.8f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
