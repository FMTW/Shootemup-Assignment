using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float turnRate;
    
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
            Debug.Log("This missile hits enemy");
            Destroy(gameObject);
        }

    }
}
