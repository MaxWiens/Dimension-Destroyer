using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MainMenuButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuPanel;
    
    [SerializeField]
    private GameObject GoToPanel;


    public void OnButtonPress()
    {
        MainMenuPanel.SetActive(false);
        GoToPanel.SetActive(true);
    }
}
