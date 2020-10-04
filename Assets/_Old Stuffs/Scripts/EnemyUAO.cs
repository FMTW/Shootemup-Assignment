using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUAO : Enemy
{
    [Header("Manuver Setting")]
    [SerializeField] private float speed;
    

    private Rigidbody rb;
    private Material material;
    private MeshCollider meshCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        glowTargets = GetComponentsInChildren<Renderer>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        material = GetComponentInChildren<Renderer>().material;
    }
    
    public override void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(Glow());
        
        if (health <= 0)
        {
            Explode();
            DropItem();
        }
    }

    public void IncreaseDifficulty(int waveCount)
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
            other.GetComponent<Player>().TakeDamage(health);
        }
    }
}
