using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructionTracker : MonoBehaviour
{
    public Transform destructibleObjects;

    public float PercentDestroyed => (initialObjectCount - destructibleObjects.childCount) / (float)initialObjectCount;

    private int initialObjectCount;
    private GamestateManager gamestateManager;
    private bool victoryTriggered;

    // Start is called before the first frame update
    void Start()
    {
        victoryTriggered = false;
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

        gamestateManager = GameObject.FindGameObjectWithTag("GamestateManager").GetComponent<GamestateManager>();

        if (gamestateManager == null)
        {
            Debug.LogError($"No GamestateManager found, victory screen will not appear at {SceneManager.GetActiveScene().path}");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!victoryTriggered && destructibleObjects.childCount == 0)
        {
            victoryTriggered = false;
            gamestateManager.SetGameStateVictory();
        }
    }
}
