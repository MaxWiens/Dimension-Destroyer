using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestructionHUD : MonoBehaviour
{
    [SerializeField, NotNull] protected DestructionTracker destructionTracker;
    [SerializeField, NotNull] protected TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = $"Destroyed: {destructionTracker.PercentDestroyed:P0}";
    }
}
