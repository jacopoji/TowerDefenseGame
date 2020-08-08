using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public GameObject GameOverUI;
    public bool isGameOver;
    public FadeEffect fadeEffect;

    public int survivedRounds;
    public Text rounds;

    private void Awake()
    {
        
        instance = this;
        GameOverUI.SetActive(false);
    }
    private void Start()
    {
        isGameOver = false;
        survivedRounds = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //temporary test function to test gameover.
            GameOver();
        }
    }

    public void CheckGameOver()
    {
        if (PlayerStats.lives <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        if(!isGameOver)
        {
        isGameOver = true;
        rounds.text = survivedRounds.ToString();
        GameOverUI.SetActive(true);

        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnRetry()
    {
        fadeEffect.PlayFadeOut(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1f;
    }

    public void OnMenu()
    {
        fadeEffect.PlayFadeOut(0);
        Time.timeScale = 1f;
    }
}
