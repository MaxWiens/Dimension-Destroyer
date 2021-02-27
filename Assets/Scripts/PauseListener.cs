using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    [SerializeField, NotNull] private InputManagerSO _inputs;
    [SerializeField, NotNull] private GameObject pausePanel;
    [SerializeField, NotNull] private GameObject hudPanel;

	private void OnEnable()
	{
		_inputs.Pause += TogglePause;
	}

	private void OnDisable()
	{
		_inputs.Pause -= TogglePause;
	}

	private void TogglePause(bool pressed)
    {
		if (pressed)
        {
			if (Time.timeScale == 0)
            {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				_inputs.ToggleUIInput(false);
				pausePanel.SetActive(false);
				hudPanel.SetActive(true);
				Time.timeScale = 1;
            }
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				_inputs.ToggleUIInput(true);
				pausePanel.SetActive(true);
				hudPanel.SetActive(false);
				Time.timeScale = 0;
			}
        }
    }
}
