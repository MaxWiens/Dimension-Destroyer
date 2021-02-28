using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OnRealityDistoryion(){
        SceneManager.LoadScene("RealLevel1");
    }

    public void OnInto(){
        SceneManager.LoadScene("RealBaseLevel2");
    }
}
