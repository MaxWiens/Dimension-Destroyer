using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField, NotNull] private InputManagerSO _inputs;

    public int energyCells;
    public int lenses;
    public List<AbstractPlayerGun> weapons;
    public int currentWeaponIndex;
    public bool shielded = true;

    public AbstractPlayerGun CurrentWeapon => weapons[currentWeaponIndex];

    private GamestateManager gamestateManager;

    private void Start()
    {
        SetActiveGun(0);

        gamestateManager = GameObject.FindGameObjectWithTag("GamestateManager").GetComponent<GamestateManager>();

        if (gamestateManager == null)
        {
            Debug.LogError($"No GamestateManager found, death screen will not appear at {SceneManager.GetActiveScene().path}");
            Destroy(gameObject);
        }
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

    private void OnDestroy()
    {
        // A warning was already printed if this was null,
        // so if it is null now and wasn't before then it is probably because we are changing scenes and
        // the GamestateManager is already deleted.
        if (gamestateManager != null)
            gamestateManager.SetGameStateDead();
    }

    public void TakeDamage()
    {
        if (shielded)
            shielded = false;
        else
            Destroy(gameObject);
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
