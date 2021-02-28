using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderBehavior : MonoBehaviour
{
    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private InputManagerSO inputManager;

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 0.4f);
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
        inputManager.Sensitivity = new Vector2(value, value);
    }
}
