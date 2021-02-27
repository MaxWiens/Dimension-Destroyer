using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject previousPanel;

    [SerializeField]
    private GameObject currentPanel;

    public void OnButtonPress()
    {
        currentPanel.SetActive(false);
        previousPanel.SetActive(true);
    }
}
