using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public FadeEffect fadeEffect;

    public void OnPlay()
    {

        fadeEffect.PlayFadeOut(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnExit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
