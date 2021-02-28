using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotsHUD : MonoBehaviour
{
    [SerializeField, NotNull] protected PlayerStats playerStats;
    [SerializeField, NotNull] protected TMP_Text equipped;

    // Update is called once per frame
    void Update()
    {
        equipped.text = $"Shots: {playerStats.CurrentWeapon.Shots:0.##}";
    }
}
