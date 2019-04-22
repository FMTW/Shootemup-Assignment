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
    [SerializeField] private GameObject OnKilledVFX;

    [Header("Fire Control")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject fireMode;
    [SerializeField] private float fireRate;
    private float nextShot;

    private Rigidbody rb;
    private MeshCollider collider;
    private float moveHorizontal, moveVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<MeshCollider>();
    }

    private void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(fireMode, muzzle.position, Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z));
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
            Instantiate(OnKilledVFX, transform.position, transform.rotation);
        }
    }
}
