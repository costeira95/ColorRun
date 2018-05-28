using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public int Score = 0; // The score of the game
    public string time; // time of the game passed stored in a string
    public float TimePassed = 0; // time passed
    public float InitialSpeed = 10f; // initial speed of the game
    public float Speed; // speed of the game in gaming
    public bool isDead = false; // if the player is dead
   public  bool isFroozen = false; // if the player is froozed

    public float FroozenTime; // time that the player is frozen

    public static float effectsVolume; // Volume of the game effects
    public static float musicVolume; // Volume of the music in game

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        Speed = InitialSpeed;
    }

    // Use this for initialization
    void Start () {
        Camera.main.GetComponent<AudioSource>().volume = musicVolume;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            TimePassed += Time.deltaTime;
            int minutes = (int)TimePassed / 60;
            int seconds = (int)TimePassed % 60;
            time = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (((int)Speed) != 30)
                Speed += Time.deltaTime * 0.2f;
        }
	}

    public void ResetScore()
    {
        if (!isDead)
        {
            CameraShaker.Instance.ShakeOnce(10f, 10f, 0.1f, 1f);
            Score = Score / 2;
            //StartCoroutine(FroozenEffect());
            Speed = InitialSpeed;
        }
    }

    public void Died()
    {
        isDead = true;
        Camera.main.GetComponent<AudioSource>().Stop();
        AdManager.count++;
        SceneManager.LoadScene("Menu");
    }

    public void Scored()
    {
        if (!isDead)
        {
            if(!isFroozen)
                Score++;
        }
    }

    public void LosePoint()
    {
        Score--;
    }

    public void Freeze()
    {
        StartCoroutine(FroozenEffect());
    }

    /***********************************************************************************
     * 
     * Corrutine freezed effect
     * 
     * ********************************************************************************/

    IEnumerator FroozenEffect()
    {
        isFroozen = true;
        Camera.main.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(FroozenTime);
        Camera.main.GetComponent<PlayableDirector>().Stop();
        Camera.main.GetComponent<FrostEffect>().enabled = false;
        isFroozen = false;
    }
}
