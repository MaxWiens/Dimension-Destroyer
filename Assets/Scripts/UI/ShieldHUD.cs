using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHUD : MonoBehaviour
{
    [NotNull, SerializeField] private Sprite fullShield;
    [NotNull, SerializeField] private Sprite emptyShield;
    [NotNull, SerializeField] private PlayerStats playerStats;
    [NotNull, SerializeField] private Image image;

    private bool lastShieldStatus;

    private void Start()
    {
        lastShieldStatus = playerStats.shielded;
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.shielded != lastShieldStatus)
        {
            lastShieldStatus = playerStats.shielded;
            SetSprite();
        }
    }

    private void SetSprite()
    {
        if (playerStats.shielded)
        {
            image.sprite = fullShield;
        }
        else
        {
            image.sprite = emptyShield;
        }
    }
}
