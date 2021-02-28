using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderBehavior : MonoBehaviour
{
    [NotNull, SerializeField]
    private Slider sensitivitySlider;

    [NotNull, SerializeField]
    private InputManagerSO inputManager;

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.value = Settings.GetSensitivity();
    }

    public void SetSensitivity(float value)
    {
        Settings.SetSensitivity(value, inputManager);
    }
}
