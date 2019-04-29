using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 tilt;
    [SerializeField] private Boundary boundary;
    
    [Header("Armory")]
    [SerializeField] private int weaponLevel;
    [SerializeField] private GameObject[] weapons;

    [Header("Main Weapon")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float fireRate;
    [SerializeField] private float multiplier;
    private float nextShot;

    [Header("Missile Launcher")]
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform launcher;
    [SerializeField] private Transform target;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float missileFireRate;
    [SerializeField] private float missileLifeTime;
    private float missileColdDown;

    [Header("Lighting")]
    [SerializeField] private GameObject[] lights;
    [SerializeField] private bool lightOn;
    [SerializeField] private float flashRate;
    
    [Header ("VFX Setting")]
    [SerializeField] private GameObject OnKilledVFX;

    private Rigidbody rb;
    private float moveHorizontal, moveVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("SetTarget", 0f, 1f);
    }

    private void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            GameObject projectileClone  = Instantiate(weapon, muzzle.position, Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z));
            Destroy(projectileClone, 1f);
        }

        if (target != null && Time.time > missileColdDown)
        {
            missileColdDown = Time.time + missileFireRate;
            GameObject missileClone = Instantiate(missile, launcher.position, launcher.rotation);
            missileClone.transform.parent = GameObject.FindGameObjectWithTag("Missiles").transform;
            missileClone.GetComponent<Missile>().SetTarget(target);
            Destroy(missileClone, missileLifeTime);
        }

    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.velocity = movement * speed;
        rb.rotation = Quaternion.Euler(moveVertical * tilt.x, 0, moveHorizontal * -tilt.y);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            transform.position.y,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectiles"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject clone = Instantiate(OnKilledVFX, transform.position, transform.rotation);
            Destroy(clone, 1f);
        }
    }


    // Upgrade player weapon
    public void UpgradeWeapon()
    {
        if (weaponLevel < (weapons.Length - 1))
        {
            weaponLevel++;
            weapon = weapons[weaponLevel];
        }
    }

    // Increase weapon fire rate
    public void IncreaseFireRate()
    {
        fireRate *= multiplier;
    }

    // Homing missile targeting function
    private void SetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && minDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
