using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonBehavior : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("Bogan");
        Debug.Log("Loading Bogan Scene");
    }
}
