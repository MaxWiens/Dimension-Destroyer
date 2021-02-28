using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructionSliderHUD : MonoBehaviour
{
    [SerializeField, NotNull] protected DestructionTracker destructionTracker;
    [SerializeField, NotNull] protected Slider slider;

    // Update is called once per frame
    void Update()
    {
        slider.value = destructionTracker.PercentDestroyed;
    }
}
