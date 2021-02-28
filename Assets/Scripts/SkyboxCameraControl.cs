using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCameraControl : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
