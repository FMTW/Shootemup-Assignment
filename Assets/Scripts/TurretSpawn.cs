using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject clone;

    private void OnBecameVisible()
    {
        clone = Instantiate(turret, transform.position, transform.rotation);
        clone.transform.parent = GameObject.FindGameObjectWithTag("Turrets").transform;
    }

    private void OnBecameInvisible()
    {
        Destroy(clone, 1f);
    }


}
