using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    private GamestateManager gamestateManager;

    [NotNull, SerializeField] private Button button;
    private void OnEnable()
    {
        button.Select();
    }

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
