using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public int totalCountToSpawn;
    public List<GameObject> toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount < totalCountToSpawn)
        {
            Debug.LogError($"RandomSpawner does not have enough child nodes to spawn all requested objects. {totalCountToSpawn} requested, {transform.childCount} children");
            Destroy(gameObject);
            return;
        }

        HashSet<Transform> used = new HashSet<Transform>();
        while (totalCountToSpawn > 0)
        {
            Transform childTransform;
            do
            {
                childTransform = transform.GetChild(Random.Range(0, transform.childCount));
            }
            while (used.Contains(childTransform));
            used.Add(childTransform);

            GameObject chosenSpawn = toSpawn[Random.Range(0, toSpawn.Count)];

            Instantiate(chosenSpawn, childTransform.position, childTransform.rotation);
            totalCountToSpawn--;
        }

        Destroy(gameObject);
    }
}
