using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [Header("Fire Control")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform[] muzzles;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    

    private float nextShot;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        glowTargets = GetComponentsInChildren<Renderer>();
    }
    
    public override void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(Glow());

        if (health <= 0)
        {
            GameObject.FindWithTag("PlayerCamera").SendMessage("Shake");
            GameObject clone = Instantiate(explosionVFX, turret.transform.position, turret.transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Explosions").transform;
            Destroy(clone, 1f);
            DropItem();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player != null)
        {
            turret.transform.LookAt(player);
            turret.transform.eulerAngles = new Vector3(0f, turret.transform.eulerAngles.y, 0f);

            if (Time.time > nextShot)
            {
                nextShot = Time.time + fireRate;
                foreach (Transform muzzle in muzzles)
                    Instantiate(projectile, muzzle.position, muzzle.rotation);
            }
        }
    }
    
}
