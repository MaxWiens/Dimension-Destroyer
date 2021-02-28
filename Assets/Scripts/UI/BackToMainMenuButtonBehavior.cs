using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuButtonBehavior : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
