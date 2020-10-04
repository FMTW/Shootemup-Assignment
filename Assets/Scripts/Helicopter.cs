using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefenseGame {
    public class Helicopter : Vehicle
    {
        [Header("Chopper Stats")]
        [SerializeField] float m_Speed;

        [Header("Chopper Config")]
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] Vector2 m_Tilt;

        private Rigidbody m_Rb;
        private float m_MoveHorizontal, m_MoveVertical;

        void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ControlHelicopter();
        }

        private void ControlHelicopter()
        {
            MoveHelicopter();
            RotateHelicopter();
        }

        /// <summary>
        /// Move Helicopter on X,Z based on player input.
        /// </summary>
        private void MoveHelicopter()
        {
            m_MoveHorizontal = Input.GetAxis("Horizontal");
            m_MoveVertical = Input.GetAxis("Vertical");

            // Tilt player based on velocity
            m_Rb.rotation = Quaternion.Euler(m_MoveVertical * m_Tilt.x, 0, m_MoveHorizontal * -m_Tilt.y);

            Vector3 _input = new Vector3(m_MoveHorizontal, 0, m_MoveVertical);

            m_Rb.velocity = _input * m_Speed;
        }

        /// <summary>
        /// Rotate Helicopter based on mouse position in the world.
        /// </summary>
        private void RotateHelicopter()
        {
            RaycastHit _hitInfo;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, m_LayerMask))
                m_Rb.transform.LookAt(new Vector3(_hitInfo.point.x, transform.position.y, _hitInfo.point.z));

        }
    }
}