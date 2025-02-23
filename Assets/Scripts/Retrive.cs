using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Retrive : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;

    // Start is called before the first frame update
    void Start()
    {
        skeletonGraphic.initialSkinName = "skin_0" + GameManager.instance.selectPlayerSkin;
        skeletonGraphic.startingAnimation = "hurt2";
        skeletonGraphic.Initialize(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevertPlayer()
    {
        GameManager.instance.RevertPlayer();
        gameObject.SetActive(false);
    }
    


    public void Close()
    {
        ShowGameOver();
        gameObject.SetActive(false);
    }

    void ShowGameOver()
    {
        
        GameManager.instance.uiManager.ShowVictoryPanel();
        GameManager.instance.uiManager.victoryPanel.ShowFail();
    }
}
