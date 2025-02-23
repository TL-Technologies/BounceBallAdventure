using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public VictoryPanel victoryPanel;

    public GamePanel gamePanel;

    public PausePanel pausePanel;

    public Retrive retrivePanel;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitUI()
    {
        ShowVictoryPanel();
        victoryPanel.ShowHome();
        gamePanel.InitUI();
        if (PlayerPrefs.GetInt("Replay") == 1)
        {
            victoryPanel.StartToPlay();
            PlayerPrefs.SetInt("Replay", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.uiChild.SetActive(true);
    }

    public void ShowRetrivePanel()
    {
        retrivePanel.gameObject.SetActive(true);
    }
}
