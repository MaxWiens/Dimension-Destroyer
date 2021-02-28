using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBehavior : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = Settings.GetVolume();
    }
    
    public void SetVolume(float volume)
    {
        Settings.SetVolume(volume);
    }
}
