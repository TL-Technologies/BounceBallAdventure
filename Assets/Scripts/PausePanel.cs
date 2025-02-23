using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject musicCheck, soundCheck;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        GameManager.instance.currentState = GameManager.STATE.PLAYING;
        gameObject.SetActive(false);
    }

    public void GoHome()
    {
        GameManager.instance.ReloadScene();
    }

    public void Replay()
    {
        GameManager.instance.ReplayScene();
    }

    public void ToggleMusic()
    {
        int musicState = PlayerPrefs.GetInt("Music");
        if (musicState == 0)
        {

            musicCheck.SetActive(false);
            PlayerPrefs.SetInt("Music", 1);
            AudioManager.instance.ToogleMusic(false);
        }
        else
        {

            musicCheck.SetActive(true);

            PlayerPrefs.SetInt("Music", 0);
            AudioManager.instance.ToogleMusic(true);
        }
    }

    public void LoadData()
    {
        int musicState = PlayerPrefs.GetInt("Music");
        if (musicState == 1)
        {

            musicCheck.SetActive(false);
            
            AudioManager.instance.ToogleMusic(false);
        }
        else
        {

            musicCheck.SetActive(true);

           
            AudioManager.instance.ToogleMusic(true);
        }

        int soundState = PlayerPrefs.GetInt("Sound");
        if (soundState == 1)
        {

            soundCheck.SetActive(false);
            
            AudioManager.instance.ToogleSound(false);

        }
        else
        {

            soundCheck.SetActive(true);

     
            AudioManager.instance.ToogleSound(true);
        }
    }

    public void ToggleSound()
    {
        int soundState = PlayerPrefs.GetInt("Sound");
        if (soundState == 0)
        {

            soundCheck.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            AudioManager.instance.ToogleSound(false);

        }
        else
        {

            soundCheck.SetActive(true);

            PlayerPrefs.SetInt("Sound", 0);
            AudioManager.instance.ToogleSound(true);
        }
    }
}
