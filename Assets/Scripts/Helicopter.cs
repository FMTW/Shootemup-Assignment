using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace TowerDefenseGame {
    public class Helicopter : Vehicle
    {
        [Header("Chopper Stats")]
        [SerializeField] float m_Speed = 0;

        [Header("Chopper Config")]
        [SerializeField] Vector2 m_Tilt = Vector2.zero;

        private Transform m_Model = null;
        private Rigidbody m_Rb = null;
        private float m_MoveHorizontal = 0;
        private float m_MoveVertical = 0;

        void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
            m_Model = transform.GetChild(0);
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
            m_Model.localRotation = Quaternion.Euler(m_MoveVertical * m_Tilt.x, 0, m_MoveHorizontal * -m_Tilt.y);

            Vector3 _input = new Vector3(m_MoveHorizontal, 0, m_MoveVertical);

            m_Rb.velocity = _input * m_Speed;
        }

        /// <summary>
        /// Rotate Helicopter based on mouse position in the world.
        /// </summary>
        private void RotateHelicopter()
        {
            Vector3 _aimpoint = CameraController.Instance.GetAimpointPosition();
            m_Rb.transform.LookAt(new Vector3(_aimpoint.x, transform.position.y, _aimpoint.z));

        }
    }
}