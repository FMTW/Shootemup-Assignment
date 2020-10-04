using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string tag;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitVFX;

    private Transform target;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        transform.LookAt(target, transform.forward);
        transform.Rotate(new Vector3(90f, 0f, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            if (tag == "Enemy")
                other.GetComponent<Enemy>().TakeDamage(damage);
            else
                other.GetComponent<Player>().TakeDamage(damage);
            GameObject clone = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(clone, 1f);
            Destroy(gameObject);
        }
    }
}
