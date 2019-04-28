using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    [Header("Powerup Types")]
    public bool weapon;
    public bool fireRate;

    private float tumble = 1f;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fireRate)
                other.GetComponentInParent<PlayerController>().IncreaseFireRate();
            else if (weapon)
                other.GetComponentInParent<PlayerController>().UpgradeWeapon();

            Destroy(gameObject);
        }
    }
}
