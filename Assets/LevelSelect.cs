using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    void OnRealityDistoryion(){
        SceneManager.LoadScene("RealLevel1");
    }

    void OnInto(){
        SceneManager.LoadScene("RealBaseLevel2");
    }
}
