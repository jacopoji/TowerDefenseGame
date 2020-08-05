using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pause;
    public Button continueButton;
    public GameMaster gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        gameMaster = GameMaster.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (gameMaster.isGameOver)
            return;
        pause.SetActive(!pause.activeSelf);
        if (pause.activeSelf)
        {
            
            Time.timeScale = 0f;
        }
        else
        {
            continueButton.transform.localScale = new Vector3(1, 1, 1); //reset continue button scale to prevent ui size bug=
            Time.timeScale = 1f;
        }
    }
}
