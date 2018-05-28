using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCode : MonoBehaviour {

    [Serializable]
    public class Score :IComparable<Score> {
        public int points;
        public string gameTime;

        public Score(int _points, string _gameTime)
        {
            points = _points;
            gameTime = _gameTime;
        }

        public int CompareTo(Score obj)
        {
            if (points < obj.points || points == obj.points
                && String.Compare(gameTime, obj.gameTime) > 0)
                return -1;
            else if (points == obj.points && String.Compare(obj.gameTime, gameTime) == 0)
                return 0;
            return 1;
        }
    }

    public GameObject MainMenuPanel;
    public GameObject PrefsPanel;
    public GameObject HighScorePanel;
    public GameObject NoScorePanel;
    public GameObject HowToPlayPanel;
    Score CurrentScore;
    Score HighScore;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0.75f);
        else
           GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        if (!PlayerPrefs.HasKey("EffectsVolume"))
            PlayerPrefs.SetFloat("EffectsVolume", 0.95f);

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            CurrentScore = new Score(gm.Score, gm.time);
            Destroy(gm.gameObject);
            HighScore = RetriveHighScore();
            if (CurrentScore.CompareTo(HighScore) > 0)
            {
                StoreHighScore();
                MainMenuPanel.SetActive(false);
                HighScorePanel.SetActive(true);
            } else
            {
                transform.Find("NoScore-Panel/Text").GetComponent<Text>().text =
                    string.Format("You caught {0} in {1} minutes\n", CurrentScore.points, CurrentScore.gameTime);
                transform.Find("Title").gameObject.SetActive(false);
                MainMenuPanel.SetActive(false);
                NoScorePanel.SetActive(true);
            }
               
        }
        else
        {
            MainMenuPanel.SetActive(true);
        }
    }

    public void StoreHighScore()
    {
        string json = JsonUtility.ToJson(CurrentScore);
        PlayerPrefs.SetString("HighScore", json);
    }

    public Score RetriveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            string json = PlayerPrefs.GetString("HighScore");
            return JsonUtility.FromJson<Score>(json);
        }
        return new Score(0, "00:00");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void ShowPrefs()
    {
        MainMenuPanel.SetActive(false);
        PrefsPanel.SetActive(true);
    }

    public void ShowHighScore()
    {
        MainMenuPanel.SetActive(false);
        HighScorePanel.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        transform.Find("Title").gameObject.SetActive(false);
        MainMenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void CancelButton()
    {
        NoScorePanel.SetActive(false);
        PrefsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        transform.Find("Title").gameObject.SetActive(true);
    }
}
