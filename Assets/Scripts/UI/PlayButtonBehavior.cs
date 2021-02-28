using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonBehavior : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadSceneAsync("Nathan");
        Debug.Log("Loading Bogan Scene");
    }
}
