using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
    public float xMin, xMax, zMin, zMax;
}

public class Player : Entity
{
    [Header("Player Setting")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float mouseSpeed;
    [SerializeField] private float keyboardSpeed;
    [SerializeField] private Vector2 tilt;
    [SerializeField] private Boundary boundary;
    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    
    [Header("Armory")]
    [SerializeField] private int weaponLevel;
    [SerializeField] private GameObject[] weapons;

    [Header("Main Weapon")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float gunFireRate;
    [SerializeField] private float multiplier;
    private float gunColdDown;

    [Header("Hydro Pod")]
    [SerializeField] private bool hydroActive = false;
    [SerializeField] private int hydroLevel;
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform[] hydroPods;
    [SerializeField] private int currentPod;
    [SerializeField] private float rocketFireRate;
    [SerializeField] private float rocketLifeTime;
    [SerializeField] private int rocketLimit;
    [SerializeField] private int rocketCount;
    private float rocketColdDown;



    [Header("Missile Launcher")]
    [SerializeField] private bool missileActive = false;
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform launcher;
    [SerializeField] private Transform target;
    [SerializeField] private float range;
    [SerializeField] private float missileFireRate;
    [SerializeField] private float missileLifeTime;
    private float missileColdDown;


    [Header("Lighting")]
    [SerializeField] private GameObject[] lights;
    [SerializeField] private float flashRate;
    [SerializeField] private float flashTime;
    
    [Header ("VFX Setting")]
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] protected float glowIntensity;
    protected Renderer[] glowTargets;


    private Rigidbody rb;
    private float moveHorizontal, moveVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        glowTargets = GetComponentsInChildren<Renderer>();
        InvokeRepeating("SetTarget", 0f, 1f);

        // Hydro pod control
        StartCoroutine(FireRocket());

        // Collision light controll
        StartCoroutine(Flash());
    }

    private void Update()
    {
        // Gun firing control
        if (Time.time > gunColdDown)
        {
            gunColdDown = Time.time + gunFireRate;
            GameObject bulletClone  = Instantiate(weapon, muzzle.position, Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z));
            Destroy(bulletClone, 1f);
        }

        // Hydro pod firing control
        // It's in Start() because it required StartCoroutine!! // This is as far as I can do for the time being


        // Missile firing control
        if (target != null && Time.time > missileColdDown && missileActive)
        {
            missileColdDown = Time.time + missileFireRate;
            GameObject missileClone = Instantiate(missile, launcher.position, launcher.rotation);
            missileClone.transform.parent = GameObject.FindGameObjectWithTag("Missiles").transform;
            missileClone.GetComponent<Missile>().SetTarget(target);
            Destroy(missileClone, missileLifeTime);
        }
    }


    // TODO
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(0))
            ControlWithMouse();
        else if (moveHorizontal != 0 || moveVertical != 0)
            ControlWithKeyboard();
        else
            rb.velocity = Vector3.zero;
        
        Debug.Log("Player velocity is " + rb.velocity);

        // Keep Player in the boundary
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            transform.position.y,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        // Tilt player based on velocity
        rb.rotation = Quaternion.Euler(moveVertical * tilt.x, 0, moveHorizontal * -tilt.y);
    }

    // Follow mouse when player click mouse left button
    void ControlWithMouse()
    {
        currentSpeed = mouseSpeed;
        Vector3 mouse = Input.mousePosition;
        mouse.z = 10f;
        mousePosition = Camera.main.ScreenToWorldPoint(mouse);
        mousePosition = new Vector3(mousePosition.x, 0, mousePosition.z);
        mouseDirection = (mousePosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, mousePosition) < .4f)
            currentSpeed = Mathf.Lerp(mouseSpeed, 0, Time.deltaTime);
        rb.velocity = new Vector3(mouseDirection.x * currentSpeed, 0, mouseDirection.z * currentSpeed);
    }

    // Control player with keyboard
    void ControlWithKeyboard()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * keyboardSpeed;
    }

    // Rocket firing control
    IEnumerator FireRocket()
    {
        while (true)
        {
            for (currentPod = 0; currentPod < hydroLevel; currentPod++)
            {
                if (currentPod == 4)
                    currentPod = 1; 
                GameObject rocketClone = Instantiate(rocket, hydroPods[currentPod].position, Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z));
                Destroy(rocketClone, rocketLifeTime);
                rocketCount++;
                yield return new WaitForSeconds(rocketFireRate);
            }

            if (rocketCount >= rocketLimit)
            {
                yield return new WaitForSeconds(rocketColdDown);
                rocketCount = 0;
            }

        }
    }
    
    // Take damage when hit by enemy projectiles
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(Glow());

        if (health <= 0)
        {
            GameObject clone = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(clone, 1f);
            Destroy(gameObject);
        }
    }

    // Increase weapon fire rate
    public void IncreaseFireRate()
    {
        gunFireRate *= multiplier;
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

    // Activate hydro pod
    public void UpgradeHydro()
    {
        if (hydroLevel < 4)
            hydroLevel++;
        rocketLimit++;
    }

    // Activate Missile
    public void ActivateMissile()
    {
        missileActive = true;
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

    // Make player glow when hit by enemy projectile
    IEnumerator Glow()
    {
        float time = 0.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            foreach (var target in glowTargets)
            {
                target.material.SetVector("_EmissionColor", Color.yellow * glowIntensity * time);
            }
            yield return null;
        }
    }

    // Make collision light flash
    IEnumerator Flash()
    {
        while (gameObject.activeSelf)
        {

            foreach (GameObject light in lights)
            {
                if (!light.activeSelf)
                {
                    light.SetActive(true);
                    yield return new WaitForSeconds(flashTime);
                    light.SetActive(false);
                }
                

            }

            yield return new WaitForSeconds(flashRate);

        }
    }

    // Draw gizmo to check mousePosition
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, mouseDirection);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mousePosition, 0.5f);
    }

    // Check missile range in scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
