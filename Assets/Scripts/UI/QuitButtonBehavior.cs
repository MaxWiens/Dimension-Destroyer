using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonBehavior : MonoBehaviour
{
    public void OnButtonPress()
    {
        Application.Quit();
    }
}
