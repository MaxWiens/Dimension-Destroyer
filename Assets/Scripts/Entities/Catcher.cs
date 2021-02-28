using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Catcher : MonoBehaviour
{
    [NotNull, SerializeField] private Transform destructible;
    [NotNull, SerializeField] private Transform indestructible;
    [NotNull, SerializeField] private NavMeshSurface n;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < 50; i++)
        {
            Transform current;
            if (destructible.childCount > 0 && (indestructible.childCount == 0 || Random.value < .8f))
                current = destructible;
            else
                current = indestructible;

            while (current.childCount > 0)
            {
                int rIndex = Random.Range(0, current.childCount);
                current = current.GetChild(rIndex);
            }

            if (NavMesh.SamplePosition(current.transform.position, out NavMeshHit hit, 4, NavMesh.AllAreas))
            {
                Vector3 finalPosition;
                if (Physics.Raycast(hit.position + new Vector3(0, 10, 0), Vector3.down, out RaycastHit rayhit, 20))
                    finalPosition = rayhit.point;
                else
                    finalPosition = hit.position;

                other.transform.position = finalPosition;
                return;
            }
        }

        Debug.LogError("Could not find location on NavMesh to place entity in 50 tries.");
    }
}
