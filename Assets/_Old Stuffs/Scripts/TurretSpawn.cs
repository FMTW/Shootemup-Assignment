using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject clone;
    [HideInInspector] public int difficulty;

    private Vector3 spawnPosition;

    private void OnBecameVisible()
    {
        RaycastHit hit;
        var down = new Vector3(0, -1, 0);
        if (Physics.Raycast(transform.position, down, out hit))
        {
            spawnPosition = new Vector3(transform.position.x, (transform.position.y - hit.distance), transform.position.z);
        }

        clone = Instantiate(turret, spawnPosition, transform.rotation);
        clone.transform.parent = GameObject.FindGameObjectWithTag("Turrets").transform;
    }

    private void OnBecameInvisible()
    {
        Destroy(clone, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
