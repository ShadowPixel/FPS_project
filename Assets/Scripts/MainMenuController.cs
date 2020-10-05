using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;

    //Start is called before frame update
    void Start()
    {
        //load highscore
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();

        FirstLoad();
    }

    private void FirstLoad()
    {
        //play song on menu start
        if (_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
        UnityEngine.Debug.Log("Data reset");
    }
    public void Quit()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Data reset");
    }
}
