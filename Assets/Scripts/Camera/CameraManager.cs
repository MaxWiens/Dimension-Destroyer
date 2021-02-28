using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform attached;

    private bool deathBackupStarted;

    private void Start()
    {
        deathBackupStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attached != null)
            transform.position = attached.position;
        else if (!deathBackupStarted)
            StartCoroutine(DeathBackup());
    }

    private IEnumerator DeathBackup()
    {
        deathBackupStarted = true;
        GetComponent<CameraControl>().enabled = false;
        Quaternion startQ = transform.rotation;
        Quaternion endQ = Quaternion.identity;
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 45;
        endQ.eulerAngles = eulerAngles;
        transform.rotation = endQ;
        Vector3 finalForward = transform.forward;
        transform.rotation = startQ;

        Vector3 startpointP = transform.position;
        Vector3 endpointP;
        if (Physics.Raycast(transform.position, -finalForward, out RaycastHit hit, 5f))
            endpointP = hit.point;
        else
            endpointP = transform.position - 5 * finalForward;

        float totalZoomDuration = 2f;
        float zoomTime = 0;

        while (zoomTime < totalZoomDuration)
        {
            zoomTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startpointP, endpointP, zoomTime / totalZoomDuration);
            transform.rotation = Quaternion.Lerp(startQ, endQ, zoomTime / totalZoomDuration);
            yield return null;
        }
    }
}
