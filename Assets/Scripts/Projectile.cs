using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] float m_Velocity = 0;
    [SerializeField] float m_Damage = 0;
    [SerializeField] float m_LiftTime = 0;

    private Rigidbody m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        Invoke("DeactivateProjectile", m_LiftTime);
    }

    void FixedUpdate()
    {
        m_RB.velocity = transform.forward * m_Velocity;
    }

    private void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }


}
