using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateManager : MonoBehaviour
{
	[SerializeField, NotNull] private InputManagerSO _inputs;
	[SerializeField, NotNull] private GameObject pausePanel;
	[SerializeField, NotNull] private GameObject hudPanel;
	[SerializeField, NotNull] private GameObject deathPanel;
	[SerializeField, NotNull] private GameObject victoryPanel;

	private enum Gamestate
    {
		Normal,
		Paused,
		Dead,
		Victory
    }

	private Gamestate state;

	private void OnEnable()
	{
		_inputs.Pause += TogglePause;
		SetGameStateNormal();
	}

	private void OnDisable()
	{
		_inputs.Pause -= TogglePause;
	}

	public void TogglePause(bool pressed)
    {
		if (pressed)
        {
			if (state == Gamestate.Normal)
				SetGameStatePaused();
			else if (state == Gamestate.Paused)
				SetGameStateNormal();
        }
    }

	private void SetGameStateNormal()
    {
		state = Gamestate.Normal;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		_inputs.ToggleGameplayInput(true);
		deathPanel.SetActive(false);
		pausePanel.SetActive(false);
		hudPanel.SetActive(true);
		victoryPanel.SetActive(false);
		Time.timeScale = 1;
	}

	private void SetGameStatePaused()
    {
		state = Gamestate.Paused;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		_inputs.ToggleGameplayInput(false);
		deathPanel.SetActive(false);
		pausePanel.SetActive(true);
		hudPanel.SetActive(false);
		victoryPanel.SetActive(false);
		Time.timeScale = 0;
	}

	public void SetGameStateDead()
    {
		state = Gamestate.Dead;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		_inputs.ToggleGameplayInput(false);
		deathPanel.SetActive(true);
		pausePanel.SetActive(false);
		hudPanel.SetActive(false);
		victoryPanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void SetGameStateVictory()
	{
		state = Gamestate.Victory;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		_inputs.ToggleGameplayInput(false);
		deathPanel.SetActive(false);
		pausePanel.SetActive(false);
		hudPanel.SetActive(false);
		victoryPanel.SetActive(true);
		Time.timeScale = 1;
	}
}
