using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject pauseMenu;

    int _currentScore;
    bool paused = false;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        //Increase score
        //TODO real implementaion
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;
                Resume();
            }
            else
            {
                paused = true;
                Pause();
                PauseTime();
            }
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update display
        _currentScoreTextView.text =
            "Current score: " + _currentScore.ToString();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public bool PauseTime()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
    }

    public void Exit()
    { 
        //compare current score to highscore
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            //save current score as highscore
            PlayerPrefs.SetInt("HighScore", _currentScore);
            UnityEngine.Debug.Log("New Highscore: " + _currentScore);
        }
        //load menu
        SceneManager.LoadScene("MainMenu");
    }
}
