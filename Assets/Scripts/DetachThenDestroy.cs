using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachThenDestroy : MonoBehaviour
{
    void Start()
    {
        transform.DetachChildren();
        Destroy(this.gameObject);
    }
}
