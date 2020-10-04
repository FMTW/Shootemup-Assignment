using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("World")]
    [SerializeField] private Transform world;
    [SerializeField] private float worldSpeed;
    [SerializeField] private float resetPoint;
    [SerializeField] protected int waveCount;

    private GameObject player;
    private Vector3 initialPosition;

    void Start()
    {
        waveCount = 0;
        world = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
            world.position -= new Vector3(0, 0, worldSpeed * Time.deltaTime);

        if (transform.position.z <= resetPoint)
        {
            transform.position = initialPosition;
            waveCount++;
        }
    }
}
