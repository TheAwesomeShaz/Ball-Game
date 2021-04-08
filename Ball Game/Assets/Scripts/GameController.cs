//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] int scoreMultiplier;
    int flag =0;//first cube has touched or not

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreMultiplierText;


    public GameObject winCanvas;
    bool timeAdded;
    public int score;
    float currentTimer;

    public string CURRENT_TAG;    

    public void AddScore(int newScoreValue, string tag)
    {
        if(flag ==0)
        {
            CURRENT_TAG = tag;
            flag = 1;
        }

        if(CURRENT_TAG == tag)
        {
            if (scoreMultiplier == 0)
            {
                scoreMultiplier = 1;
                score += newScoreValue * 1;
                StartCoroutine("ComboTimerCountDown");
            }
            if (scoreMultiplier == 1)
            {
                scoreMultiplier = 2;
                score += newScoreValue * scoreMultiplier;
                StartCoroutine("ComboTimerCountDown");
            }
            else if (scoreMultiplier == 2)
            {
                scoreMultiplier = 3;
                score += newScoreValue * scoreMultiplier;
                StopCoroutine("ComboTimerCountDown");
            }
            else if (scoreMultiplier >= 3)
            {
                scoreMultiplier = 4;
                score += newScoreValue * scoreMultiplier;
                StopCoroutine("ComboTimerCountDown");
            }
            UpdateScore();
            StartCoroutine("ComboTimerCountDown");
        }
        else
        {
            scoreMultiplier = 0;
            score += newScoreValue * 1;
            StartCoroutine("ComboTimerCountDown");
            UpdateScore();
            CURRENT_TAG = tag;
        }

    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    IEnumerator ComboTimerCountDown()
    {
        yield return new WaitForSeconds(10);
        scoreMultiplier = 0;
    }

    IEnumerator LoadLevelAfterTime(int levelIndex)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(levelIndex);
    }

   



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
        if(SceneManager.GetActiveScene().buildIndex == 0) //i.e if splash Screen
        {
            StartCoroutine(LoadLevelAfterTime(1));
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)//i.e if it is main menu scene
        {
            // nah dont do anything
        }

        else
        {
        instance = this;
        winCanvas.SetActive(false);
        currentTimer = 60;
        scoreMultiplier = 0;
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownTimer();



        if (scoreMultiplier > 1)
        {
            scoreMultiplierText.gameObject.SetActive(true);
            scoreMultiplierText.text = "x" + scoreMultiplier.ToString();
        }
        else if (scoreMultiplier == 0)
        {
            scoreMultiplierText.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (GameFinished())
        {

            if (!timeAdded && currentTimer > 0)
            {
                score += (int)currentTimer;
                timeAdded = true;
            }

            finalScoreText.text = "Congrats! \nYour Score: " + score.ToString();
            winCanvas.SetActive(true);
            timerText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            scoreMultiplierText.gameObject.SetActive(false);
            StartCoroutine(LoadLevelAfterTime(1));
            Player.instance.controlsEnabled = false;

            SetScore();
                
            
        }
        if (currentTimer < 0)
        {
            finalScoreText.text = "Congrats! \nYour Score: " + score.ToString();
            winCanvas.SetActive(true);
            timerText.gameObject.SetActive(false);
            scoreMultiplierText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            Player.instance.controlsEnabled = false;
            StartCoroutine(LoadLevelAfterTime(1));

            SetScore();
        }
    }

    private void SetScore()
    {
        if(PlayerPrefs.GetInt("score",0) < score)
        {
            PlayerPrefs.SetInt("score", score);

        }
    }

    private void CountdownTimer()
    {
        if (!GameFinished()) currentTimer -= Time.deltaTime;



        //TODO: Make UI using TMP
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        TimerShet();
        }

    }

    private void TimerShet()
    {
        if (currentTimer >= 60)
        {
            timerText.text = "1:" + ((int)60 - currentTimer).ToString();
        }
        else if (currentTimer < 10)
        {
            timerText.text = "0:0" + ((int)currentTimer).ToString();
        }
        else
        {
            timerText.text = "0:" + ((int)currentTimer).ToString();
        }
    }

    private bool GameFinished()
    {
        int deadCubes = 0;
        bool gameFinished = false;
        Cube[] cubes = FindObjectsOfType<Cube>();
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i].isDead)
            {
                deadCubes++;
            }
            if(deadCubes == cubes.Length)
            {
                gameFinished = true;
            }
        }

        Debug.Log(gameFinished);
        return gameFinished;
    }
}
