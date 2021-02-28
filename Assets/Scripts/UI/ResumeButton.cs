using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    private GamestateManager gamestateManager;

    private void Start()
    {
        gamestateManager = GameObject.FindGameObjectWithTag("GamestateManager").GetComponent<GamestateManager>();

        if (gamestateManager == null)
        {
            Debug.LogError($"No GamestateManager found, pause menu resume button will not work in scene at {SceneManager.GetActiveScene().path}");
            Destroy(gameObject);
        }
    }

    public void OnClick()
    {
        gamestateManager.TogglePause(true);
    }
}
