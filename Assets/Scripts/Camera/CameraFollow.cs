using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform attached;

    // Update is called once per frame
    void Update()
    {
        if (attached != null)
            transform.position = attached.position;
    }
}
