using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamProjectile : MonoBehaviour
{
    private GameObject user;

    private void Start()
    {
        StartCoroutine(ResizeAndDestroy());
    }

    private IEnumerator ResizeAndDestroy()
    {
        IEnumerator scaleCoroutine = ChangeScaleXZ(2f);
        StartCoroutine(scaleCoroutine);
        yield return new WaitForSeconds(0.5f);

        StopCoroutine(scaleCoroutine);
        scaleCoroutine = ChangeScaleXZ(-2f);
        StartCoroutine(scaleCoroutine);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void SetPosition(GameObject user, Transform aimTransform)
    {
        this.user = user;
        transform.position = user.transform.position + aimTransform.forward * 26f;
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = aimTransform.transform.rotation.eulerAngles + new Vector3(90, 0, 0);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != user)
        {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ChangeScaleXZ(float scalePerSecond)
    {
        while (true)
        {
            float actualChange = scalePerSecond * Time.deltaTime;
            transform.localScale += new Vector3(actualChange, 0, actualChange);
            yield return null;
        }
    }
}
