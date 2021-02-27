﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputManager", menuName = "GameJam/InputManager", order = 0)]
public class InputManagerSO : ScriptableObject, GameInputs.IGameplayActions, GameInputs.IAlwaysActions {
	[SerializeField]
	public bool InvertXCamera;
	[SerializeField]
	public bool InvertYCamera;
	[SerializeField]
	public Vector2 Sensitivity = new Vector2(0.5f,0.5f);
	public event UnityAction<Vector2> Moved;
	public event UnityAction<bool> Jump;
	public event UnityAction<Vector2> CameraRotated;
	public event UnityAction<bool> Shoot;
	public event UnityAction<bool> Pause;
	private GameInputs _gameInputs;

	private void OnEnable() {
		if(_gameInputs == null) {
			_gameInputs = new GameInputs();
			_gameInputs.Gameplay.SetCallbacks(this);
			_gameInputs.Always.SetCallbacks(this);
			_gameInputs.Enable();
		}
	}

	public void ToggleUIInput(bool enabled) {
		if (enabled)
        {
			_gameInputs.Gameplay.Disable();
        }
		else
        {
			_gameInputs.Gameplay.Enable();
        }
	}

	public void OnJump(InputAction.CallbackContext context) {
		Jump?.Invoke(context.phase == InputActionPhase.Performed);
	}

	public void OnMove(InputAction.CallbackContext context) {
		Moved?.Invoke(context.ReadValue<Vector2>());
	}

	public void OnShoot(InputAction.CallbackContext context)
	{
		Shoot?.Invoke(context.phase == InputActionPhase.Performed);
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		Pause?.Invoke(context.phase == InputActionPhase.Performed);
	}

	public void OnRotateCamera(InputAction.CallbackContext context) {
		Vector2 v = context.ReadValue<Vector2>();
		if(InvertXCamera)
			v.x = -v.x;
		if(!InvertYCamera)
			v.y = -v.y;
		CameraRotated?.Invoke(v*Sensitivity);
	}

	public void OnToggleCameraLock(InputAction.CallbackContext context)
	{
		if(context.phase == InputActionPhase.Performed){
			if(Cursor.lockState == CursorLockMode.Locked){
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}else{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}
}