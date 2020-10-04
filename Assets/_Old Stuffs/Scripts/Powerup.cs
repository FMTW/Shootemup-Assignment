using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    [Header("Powerup Types")]
    [SerializeField] string powerupType;
    private float tumble = 1f;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerupType == "Firerate")
                other.GetComponent<Player>().IncreaseFireRate();
            else if (powerupType == "Weapon")
                other.GetComponent<Player>().UpgradeWeapon();
            else if (powerupType == "Hydro")
                other.GetComponent<Player>().UpgradeHydro();
            else if (powerupType == "Missile")
                other.GetComponent<Player>().ActivateMissile();
            Destroy(gameObject);
        }
    }
}
