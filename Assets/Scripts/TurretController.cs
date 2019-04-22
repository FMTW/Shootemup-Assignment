using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Transform player;
    [SerializeField] private Transform turret;
    [SerializeField] private MeshCollider collider;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    private float nextShot;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        collider = GetComponentInChildren<MeshCollider>();
    }

    void Update()
    {
        turret.LookAt(player);
        turret.eulerAngles = new Vector3(0f, turret.eulerAngles.y, 0f);
        
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(projectile, muzzle.position, muzzle.rotation); //new Vector3(muzzle.position.x, player.position.y, muzzle.position.z)
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectiles"))
        {
            health -= 10;
            Destroy(other.gameObject);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
