using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float turnRate;
    [SerializeField] private GameObject hitVFX;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        if (target != null)
        {
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, (Quaternion.LookRotation(target.position - transform.position)), turnRate));
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Turret>().TakeDamage(gameObject, damage);
            GameObject clone = Instantiate(hitVFX, transform.position, transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Explosions").transform;
            Destroy(clone, 1f);
            Destroy(gameObject);
        }

    }
}
