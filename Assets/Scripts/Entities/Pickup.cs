using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        EnergyCell,
        Lens,
    }

    public PickupType pickupType;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
        if (stats != null)
        {
            switch(pickupType)
            {
                case PickupType.EnergyCell:
                    stats.energyCells++;
                    break;
                case PickupType.Lens:
                    stats.lenses += 3;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
