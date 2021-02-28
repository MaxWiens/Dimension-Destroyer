using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsApplier : MonoBehaviour
{
    [NotNull, SerializeField]
    private InputManagerSO inputManager;

    // Start is called before the first frame update
    void Start()
    {
        Settings.SetSensitivity(Settings.GetSensitivity(), inputManager);
        Settings.SetVolume(Settings.GetVolume());
        Destroy(gameObject);
    }
}
