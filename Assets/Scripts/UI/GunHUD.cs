using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunHUD : MonoBehaviour
{
    [SerializeField, NotNull] protected PlayerStats playerStats;
    [SerializeField, NotNull] protected TMP_Text equipped;

    // Update is called once per frame
    void Update()
    {
        equipped.text = $"Equipped: {playerStats.CurrentWeapon.Name}";
    }
}
