using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int energyCells;
    public int lenses;
    public List<AbstractGun> weapons;
    public int currentWeaponIndex;

    [SerializeField, NotNull] private InputManagerSO _inputs;

    private void Start()
    {
        SetActiveGun(0);
    }

    private void OnEnable()
    {
        _inputs.NextGun += NextGun;
        _inputs.PreviousGun += PreviousGun;
    }

    private void OnDisable()
    {
        _inputs.NextGun -= NextGun;
        _inputs.PreviousGun -= PreviousGun;
    }

    public void SetActiveGun(int index)
    {
        currentWeaponIndex = index;
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].enabled = i == currentWeaponIndex;
        }
    }

    private void NextGun(bool pressed)
    {
        if (!pressed)
            return;

        int next = currentWeaponIndex + 1;
        if (next >= weapons.Count)
            next = 0;
        SetActiveGun(next);
    }

    private void PreviousGun(bool pressed)
    {
        if (!pressed)
            return;

        int next = currentWeaponIndex - 1;
        if (next < 0)
            next = weapons.Count - 1;
        SetActiveGun(next);
    }
}
