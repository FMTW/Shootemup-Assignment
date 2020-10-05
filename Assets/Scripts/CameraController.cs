using System.Collections;
using System.Collections.Generic;
using TowerDefenseGame;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform m_Target = null;
    [SerializeField] float m_HeightOffset = 10;
    [SerializeField] LayerMask m_LayerMask;

    private void LateUpdate()
    {
        transform.position = m_Target.position + new Vector3(0, m_HeightOffset, 0);
    }

    public Vector3 GetAimpointPosition()
    {
        RaycastHit _hitInfo;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, m_LayerMask))
            return _hitInfo.point;
        else
        {
            Debug.LogWarning("Camera: Can't find hitpoint from the map!");
            return Vector3.zero;
        }
    }

}
