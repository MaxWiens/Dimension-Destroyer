using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonBehavior : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject mainmenu;
    [NotNull, SerializeField] private Button button;
    private void OnEnable()
    {
        button.Select();
    }

    public void OnButtonPress()
    {
        mainmenu.SetActive(false);
        levelSelect.SetActive(true);
        //SceneManager.LoadSceneAsync("RealLevel1");
        //Debug.Log("Loading Bogan Scene");
    }
}
