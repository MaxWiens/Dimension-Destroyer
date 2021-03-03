using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OnRealityDistoryion(){
        SceneManager.LoadScene("TheBigLevel");
    }

    public void OnInto(){
        SceneManager.LoadScene("IntroLevel");
    }
}
