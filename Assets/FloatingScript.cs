using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotation;

    [SerializeField]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotation.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
