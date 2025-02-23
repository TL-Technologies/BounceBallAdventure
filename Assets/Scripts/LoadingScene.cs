using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public float loadingTimer;

    protected float currentTimer;

    public Image loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0.0f;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        loadingBar.fillAmount = (float)currentTimer / (float)loadingTimer;

        if(currentTimer >= loadingTimer)
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel");

            if(currentLevel == 0)
            {
                currentLevel = 1;
                SceneManager.LoadScene("Level_0" + currentLevel);
            }
            else if(currentLevel > 0 && currentLevel <= 9)
            {
                SceneManager.LoadScene("Level_0" + currentLevel);
            }
            else
            {
                SceneManager.LoadScene("Level_" + currentLevel);
            }
        }
    }

   
}
