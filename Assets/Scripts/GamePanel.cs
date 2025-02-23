using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : MonoBehaviour
{
    public GameObject magnetObj;

    public TextMeshProUGUI lifeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitUI()
    {
        lifeText.text = GameManager.instance.life.ToString();
        magnetObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        GameManager.instance.currentState = GameManager.STATE.PAUSING;
        GameManager.instance.uiManager.pausePanel.gameObject.SetActive(true);
    }

    public void AddMoreLife(int _value)
    {
        GameManager.instance.life += _value;
        lifeText.text = GameManager.instance.life.ToString();
    }

    public void ShowMagnet()
    {
        magnetObj.SetActive(true);
    }
}
