using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOnAwake : MonoBehaviour
{

    [NotNull, SerializeField] private Button button;
    private void OnEnable()
    {
        button.Select();
    }
}
