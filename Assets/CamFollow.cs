using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    Transform targetToFollow;

    void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(targetToFollow.position.x - 10f, 10f, targetToFollow.position.z - 10f);
    }
}
