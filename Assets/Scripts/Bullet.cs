using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void Update()
    {
        if (!gameObject.GetComponentInChildren<MeshRenderer>().isVisible)
            Destroy(gameObject);
    }
}
