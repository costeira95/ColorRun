using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preferences : MonoBehaviour {

    private void Awake()
    {
        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float EffectsVolume = PlayerPrefs.GetFloat("EffectsVolume");

        transform.Find("Effects/Slider").GetComponent<Slider>().value = EffectsVolume;
        transform.Find("Music/Slider").GetComponent<Slider>().value = MusicVolume;
    }


    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        GameObject.Find("Canvas").GetComponent<AudioSource>().volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("EffectsVolume", volume);
        transform.GetComponentInParent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().Play();
    }
}
