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

    int _currentScore;

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
            ExitLevel();
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

    public void ExitLevel()
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
