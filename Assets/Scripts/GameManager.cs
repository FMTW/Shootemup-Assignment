using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("World")]
    [SerializeField] private Transform world;
    [SerializeField] private float worldSpeed;

    private GameObject player;

    void Start()
    {
        world = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
            world.position -= new Vector3(0, 0, worldSpeed * Time.deltaTime);
    }
}
