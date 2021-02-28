using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    private PauseListener pauseListener;

    private void Start()
    {
        pauseListener = GameObject.FindGameObjectWithTag("Pause listener").GetComponent<PauseListener>();

        if (pauseListener == null)
        {
            Debug.LogError($"No pause listener found, pause menu resume button will not work in scene at {SceneManager.GetActiveScene().path}");
            Destroy(gameObject);
        }
    }

    public void OnClick()
    {
        pauseListener.TogglePause(true);
    }
}
