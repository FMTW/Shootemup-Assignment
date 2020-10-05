using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenseGame
{
    public class Weapon : MonoBehaviour
    {
        [Header("Weapon Config")]
        [SerializeField] Transform[] m_Muzzles = null;
        [SerializeField] GameObject m_Projectile = null;
        [SerializeField] GameObject m_Casing = null;
        [SerializeField] float m_RoundPerMinute = 0;

        private float m_NextShotTimer = 0;

        private void Update()
        {
            AimWeapon();
            if (Input.GetKey(KeyCode.Space))
                FireWeapon();
        }

        private void AimWeapon()
        {
            transform.LookAt(CameraController.Instance.GetAimpointPosition());
        }

        private void FireWeapon()
        {
            GameObject _clone = null;

            if (Time.time > m_NextShotTimer)
            {
                foreach (Transform _muzzle in m_Muzzles)
                    _clone = Instantiate(m_Projectile, _muzzle.position, _muzzle.rotation);

                //GameObject _projectile = PoolManager.Instance.RequestAvailableObject(m_ProjectileType.name, "ProjectilePools");
                //_projectile.transform.position = _muzzle.position;
                //_projectile.transform.rotation = _muzzle.rotation;
                //_projectile.transform.Rotate(new Vector3(0, 0, Random.Range(-m_Dispersion, m_Dispersion)));
                //_projectile.SetActive(true);

                //AudioManager.Instance.weaponAudioSource.PlayOneShot(m_WeaponSFX);

                m_NextShotTimer = Time.time + (60 / m_RoundPerMinute);
            }

            Destroy(_clone, 5f);
        }

    }
}