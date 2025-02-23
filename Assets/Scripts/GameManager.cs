using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    public SkinData skinData;

    public static GameManager instance;

    public int levelIndex;

    public int selectPlayerSkin;

    [HideInInspector]
    public int coin, life;

    public enum STATE
    {
        HOME,
        PLAYING,
        PAUSING,
        LEVEL_FINISH,
        LEVEL_FAIL
    }

    public STATE currentState;

    public bool getMagenet;

    public BallController mainBall;

    public List<Vector3> checkPointList;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            instance = this;
           
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        skinData.RefreshData();

        currentState = STATE.HOME;

        getMagenet = false;

        checkPointList = new List<Vector3>();

        checkPointList.Add(mainBall.transform.position);

        InitFirstData();

        uiManager.InitUI();

        PlayerPrefs.SetInt("CurrentLevel", levelIndex);

      //  if(AdsControl.Instance != null)
      //   AdsControl.Instance.ShowBannerAd();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitFirstData()
    {
        int firstData = PlayerPrefs.GetInt("FirstData");

        if(firstData == 0)
        {
            coin = 200;
            life = 1;
            SaveData();
            PlayerPrefs.SetInt("FirstData", 1);
        }
        else
        {
            coin = PlayerPrefs.GetInt("Coin");
            life = PlayerPrefs.GetInt("Life");
        }

        if (life == 0)
            life = 1;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Life", life);
    }

    public void ReloadScene()
    {
        if(levelIndex < 10)
             SceneManager.LoadScene("Level_0" + levelIndex);
        else
            SceneManager.LoadScene("Level_" + levelIndex);
    }

    public void ReplayScene()
    {
        PlayerPrefs.SetInt("Replay", 1);
        ReloadScene();
    }

    public void NextScene()
    {
        PlayerPrefs.SetInt("Replay", 1);
        levelIndex++;
        if (levelIndex < 10)
            SceneManager.LoadScene("Level_0" + levelIndex);
        else
            SceneManager.LoadScene("Level_" + levelIndex);
    }

    public void AddCoin(int _value)
    {
        coin += _value;
        uiManager.victoryPanel.UpdateCoinPanel();
    }

    public void AddLife(int _value)
    {
        life += _value;
        uiManager.victoryPanel.UpdateCoinPanel();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnIE());
    }

    public void RevertPlayer()
    {
        life = 1;
        uiManager.victoryPanel.UpdateCoinPanel();
        uiManager.gamePanel.InitUI();
        mainBall.gameObject.SetActive(true);
        mainBall.transform.position = checkPointList[checkPointList.Count - 1];
        mainBall.GetComponent<BallController>().ResumeTheBall();
    }

    IEnumerator RespawnIE()
    {
        yield return new WaitForSeconds(1.0f);
        mainBall.gameObject.SetActive(true);
        mainBall.transform.position = checkPointList[checkPointList.Count - 1];
        mainBall.GetComponent<BallController>().ResumeTheBall();
    }

    public void GetBonusLife()
    {
        life += 2;
        uiManager.victoryPanel.UpdateCoinPanel();
        uiManager.gamePanel.InitUI();
        SaveData();
    }

    public void GetBonusCoin()
    {
        coin += 100;
        uiManager.victoryPanel.UpdateCoinPanel();
        uiManager.gamePanel.InitUI();
        SaveData();
    }

    public void GetMagnet()
    {
        GameManager.instance.uiManager.gamePanel.ShowMagnet();
        GameManager.instance.getMagenet = true;
    }
}
