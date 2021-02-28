using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject previousPanel;

    [SerializeField]
    private GameObject currentPanel;

    [NotNull, SerializeField] private Button button;
    private void OnEnable()
    {
        button.Select();
    }

    public void OnButtonPress()
    {
        currentPanel.SetActive(false);
        previousPanel.SetActive(true);
    }
}
