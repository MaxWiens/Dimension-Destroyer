using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonBehavior : MonoBehaviour
{
    [NotNull, SerializeField] private Button button;
    private void OnEnable()
    {
        button.Select();
    }

    public void OnButtonPress()
    {
        SceneManager.LoadSceneAsync("RealLevel1");
        Debug.Log("Loading Bogan Scene");
    }
}
