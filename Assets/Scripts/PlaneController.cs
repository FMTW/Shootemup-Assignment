using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;

    [Header("VFXs")]
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject killedVFX;
    private Material material;

    private Rigidbody rb;
    [SerializeField] private MeshCollider meshCollider;

    [Header("Test")]
    [SerializeField] private GameObject powerup;
    [SerializeField] private GameObject stars;
    public bool move;
    public int difficulty;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        rb.velocity = transform.forward * speed;
        material = GetComponentInChildren<Renderer>().material;

        if (move)
            StartCoroutine(Maneuver());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectiles"))
        {
            health -= 10;
            Instantiate(hitVFX, other.transform.position, other.transform.rotation);
            StartCoroutine(Glow());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponentInParent<PlayerController>().TakeDamage(health);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            //GameObject.FindWithTag("PlayerCamera").SendMessage("Shake");
            Instantiate(killedVFX, transform.position, transform.rotation);
            DropItem();
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

    IEnumerator Maneuver()
    {
        yield return new WaitForSeconds(0.5f);
        
        while(false)
        {

            yield return null;
        }

    }

    void DropItem()
    {
        if (Random.value < 0.2f)
        {
            Instantiate(powerup, transform.position, transform.rotation);
        }

        switch (difficulty)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

            default:
                break;
        }
    }
}
