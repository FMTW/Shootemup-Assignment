using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] private float health;

    [Header("Fire Control")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    private float nextShot;

    [Header("VFX Setting")]
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject breach;

    [Header("Loot")]
    [SerializeField] private GameObject powerup;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        turret.transform.LookAt(player);
        turret.transform.eulerAngles = new Vector3(0f, turret.transform.eulerAngles.y, 0f);

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
            GameObject clone = Instantiate(hitVFX, other.transform.position, other.transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Explosions").transform;
            Destroy(clone, 2f);
            Destroy(other.gameObject);
            StartCoroutine(Glow());
        }

        if (health <= 0)
        {
            GameObject.FindWithTag("PlayerCamera").SendMessage("Shake");
            GameObject clone = Instantiate(explosionVFX, turret.transform.position, turret.transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Explosions").transform;
            Destroy(clone, 2f);
            Destroy(gameObject);
        }
    }

    IEnumerator Glow()
    {
        float time = 0.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            breach.GetComponent<Renderer>().material.SetVector("_EmissionColor", Color.yellow * 1f * time);

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
