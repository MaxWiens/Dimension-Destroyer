using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInitializer : MonoBehaviour
{
    public bool disableMouse = true;

    private void Awake()
    {
        if (disableMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
