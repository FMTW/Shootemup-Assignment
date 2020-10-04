using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenseGame {
    public class Helicopter : Vehicle
    {
        [Header("Chopper Stats")]
        [SerializeField] float m_Speed;

        [Header("Chopper Config")]
        [SerializeField] Vector2 m_Tilt;

        private Rigidbody m_Rb;
        private float m_MoveHorizontal, m_MoveVertical;

        void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ControlCopterMovement();
        }

        private void ControlCopterMovement()
        {
            m_MoveHorizontal = Input.GetAxis("Horizontal");
            m_MoveVertical = Input.GetAxis("Vertical");

            // Tilt player based on velocity
            m_Rb.rotation = Quaternion.Euler(m_MoveVertical * m_Tilt.x, 0, m_MoveHorizontal * -m_Tilt.y);

            Vector3 _input = new Vector3(m_MoveHorizontal, 0, m_MoveVertical).normalized;

            m_Rb.velocity = _input * m_Speed;

        }
    }
}