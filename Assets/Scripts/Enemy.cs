using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Basic Setting")]
    [SerializeField] private float health;
    [SerializeField] private float speed;

    [Header("VFX Setting")]
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject killedVFX;
    private Material material;


    [Header("Loot")]
    [SerializeField] private GameObject powerup;


    private Rigidbody rb;
    private MeshCollider meshCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        meshCollider = GetComponentInChildren<MeshCollider>();
        material = GetComponentInChildren<Renderer>().material;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectiles"))
        {
            health -= 10;
            GameObject clone = Instantiate(hitVFX, other.transform.position, other.transform.rotation);
            Destroy(clone, 2f);
            StartCoroutine(Glow());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameObject clone = Instantiate(killedVFX, transform.position, transform.rotation);
            Destroy(clone, 2f);
            DropItem();
            Destroy(gameObject);
            other.GetComponentInParent<PlayerController>().TakeDamage(health);
        }

        if (health <= 0)
        {
            GameObject clone = Instantiate(killedVFX, transform.position, transform.rotation);
            Destroy(clone, 2f);
            DropItem();
            Destroy(gameObject);
        }
    }

    IEnumerator Glow()
    {
        float time = 0.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            material.SetVector("_EmissionColor", Color.yellow * 0.75f * time);

            yield return null;
        }
    }

    void DropItem()
    {
        if (Random.value < 0.2f)
        {
            GameObject clone = Instantiate(powerup, transform.position, transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Powerups").transform;
        }
    }
}
