﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{
    [SerializeField, NotNull] private EnviornmentManagerSO _enviornmentManager = default;

    private readonly float timeToEnableCollider = 1 / 3f;
    private readonly float timeToExplode = 3f;

    private bool touched;

    public float maxScale = 9f;
    public float expansionTime = 3f;
    public float contractionTime = 1 / 3f;
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
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        IEnumerator scaleCoroutine = ChangeScale(maxScale / expansionTime);
        StartCoroutine(scaleCoroutine);
        yield return new WaitForSeconds(expansionTime);

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x * sphereCollider.radius);
        HashSet<GameObject> toDelete = new HashSet<GameObject>();
        foreach (Collider c in colliders)
        {
            toDelete.Add(c.gameObject);
        }
        StopCoroutine(scaleCoroutine);
        StartCoroutine(ChangeScale(-maxScale / contractionTime));
        yield return new WaitForSeconds(contractionTime);

        foreach (GameObject g in toDelete)
        {
            if (!g.CompareTag("Void immune"))
            {
                Destroy(g);
            }
        }
        Destroy(gameObject);
        if(toDelete.Count > 0)
            _enviornmentManager.RebuildNavMesh();
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
