using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public int totalCountToSpawn;
    public List<GameObject> toSpawn;
    public DestructionTracker tracker;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 10f;

    private float _spawnTimer = 0f;
    private float _spawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn(totalCountToSpawn);
        _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        //Destroy(gameObject);
    }

    private void Update() {
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= _spawnTime){
            _spawnTimer -= _spawnTime;
            int numToSpawn = Mathf.Clamp((int)(tracker.PercentDestroyed * transform.childCount), 2, transform.childCount);
            Spawn(numToSpawn);
            _spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    public void Spawn(int amnt){
        if (transform.childCount < amnt)
        {
            Debug.LogError($"RandomSpawner does not have enough child nodes to spawn all requested objects. {amnt} requested, {transform.childCount} children");
            Destroy(gameObject);
            return;
        }

        HashSet<Transform> used = new HashSet<Transform>();
        while (amnt > 0)
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
            amnt--;
        }
    }
}
