using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTracker : MonoBehaviour
{
    public Transform destructibleObjects;

    public float PercentDestroyed => (initialObjectCount - destructibleObjects.childCount) / (float)initialObjectCount;

    private int initialObjectCount;

    // Start is called before the first frame update
    void Start()
    {
        initialObjectCount = destructibleObjects.childCount;
        int childCount = initialObjectCount;

        for (int i = 0; i < childCount; i++)
        {
            if (destructibleObjects.GetChild(i).CompareTag("Void immune"))
            {
                initialObjectCount--;
                Debug.LogError($"Void immune object {destructibleObjects.GetChild(i)} is in destructable objects");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destructibleObjects.childCount == 0)
        {
            Debug.Log("All destructable objects are destoryed, changing levels (TODO)");
        }
    }
}
