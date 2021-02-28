using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ChangePanelButton : MonoBehaviour
{
    [SerializeField]
    private GameObject FromPanel;
    
    [SerializeField]
    private GameObject GoToPanel;


    public void OnButtonPress()
    {
        FromPanel.SetActive(false);
        GoToPanel.SetActive(true);
    }
}
