using UnityEngine;

public static class Settings
{
    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat("volume", 0.75f);
    }

    public static void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        AudioListener.volume = volume;
    }

    public static float GetSensitivity()
    {
        return PlayerPrefs.GetFloat("sensitivity", 0.4f);
    }

    public static void SetSensitivity(float value, InputManagerSO inputManager)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
        inputManager.Sensitivity = new Vector2(value, value);
    }
}
