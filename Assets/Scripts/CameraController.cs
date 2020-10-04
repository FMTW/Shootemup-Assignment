using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform m_Target = null;
    [SerializeField] float m_HeightOffset = 10;

    private void LateUpdate()
    {
        transform.position = m_Target.position + new Vector3(0, m_HeightOffset, 0);
    }
}
