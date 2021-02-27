﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{
    private readonly float timeToEnableCollider = 1 / 3f;
    private readonly float timeToExplode = 3f;
    private bool touched;
    // Start is called before the first frame update
    void Start()
    {
        touched = false;
        StartCoroutine(EnableCollider());
        StartCoroutine(FallbackExplosionTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!touched)
        {
            touched = true;
            StartCoroutine(DoEffect());
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(timeToEnableCollider);
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.enabled = true;
    }

    private IEnumerator FallbackExplosionTimer()
    {
        yield return new WaitForSeconds(timeToExplode);
        if (!touched)
        {
            touched = true;
            StartCoroutine(DoEffect());
        }
    }

    private IEnumerator DoEffect()
    {
        IEnumerator scaleCoroutine = ChangeScale(3f);
        StartCoroutine(scaleCoroutine);
        yield return new WaitForSeconds(3f);

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x * sphereCollider.radius);
        HashSet<GameObject> toDelete = new HashSet<GameObject>();
        foreach (Collider c in colliders)
        {
            toDelete.Add(c.gameObject);
        }
        StopCoroutine(scaleCoroutine);
        StartCoroutine(ChangeScale(-27f));
        yield return new WaitForSeconds(1 / 3f);

        foreach (GameObject g in toDelete)
        {
            Destroy(g);
        }
        Destroy(gameObject);
    }

    private IEnumerator ChangeScale(float scalePerSecond)
    {
        while(true)
        {
            float actualChange = scalePerSecond * Time.deltaTime;
            transform.localScale += new Vector3(actualChange, actualChange, actualChange);
            yield return null;
        }
    }
}