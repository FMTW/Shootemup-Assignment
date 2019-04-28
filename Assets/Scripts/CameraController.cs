using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float intensity;
    [SerializeField] private Transform mainCam;

    
    void LateUpdate()
    {
        transform.position = player.position;
    }

    IEnumerator Shake()
    {
        float t = 0.5f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float randonX = Random.Range(-intensity, intensity) * t;
            float randonY = Random.Range(-intensity, intensity) * t;

            mainCam.transform.localPosition = new Vector3(randonX, randonY, 0);

            yield return null;
        }
    }
}
