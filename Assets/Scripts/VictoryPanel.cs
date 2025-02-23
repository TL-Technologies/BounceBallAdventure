using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;

public class VictoryPanel : MonoBehaviour
{

    public GameObject homeTitle, victoryTitle, failTitle;

    public GameObject coinPanel;

    public TextMeshProUGUI coinTxt, lifeTxt;

    public TextMeshProUGUI levelTxt;

    public GameObject mask;

    public GameObject uiChild;

    public SkeletonGraphic skeletonGraphic;

    public GameObject tryBtn, buyBtn, getBtn;

    public TextMeshProUGUI characterPriceTxt;

    private int skinIndex;

    public string gameUrl, pageUrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshCharacter()
    {
        skeletonGraphic.initialSkinName = "skin_0" + skinIndex;
        skeletonGraphic.Initialize(true);

        if(GameManager.instance.skinData.skinList[skinIndex].currentType == SkinData.Skin.TYPE.UNLOCKED)
        {
            tryBtn.SetActive(false);
            buyBtn.SetActive(false);
            getBtn.SetActive(false);
        }
        else if (GameManager.instance.skinData.skinList[skinIndex].currentType == SkinData.Skin.TYPE.GET_BY_RW)
        {
            tryBtn.SetActive(false);
            buyBtn.SetActive(false);
            getBtn.SetActive(true);
        }
        else if (GameManager.instance.skinData.skinList[skinIndex].currentType == SkinData.Skin.TYPE.TRY_BY_RW)
        {
            tryBtn.SetActive(true);
            buyBtn.SetActive(false);
            getBtn.SetActive(false);
        }

        else if (GameManager.instance.skinData.skinList[skinIndex].currentType == SkinData.Skin.TYPE.BUY_BY_COIN)
        {
            tryBtn.SetActive(false);
            buyBtn.SetActive(true);
            getBtn.SetActive(false);

            characterPriceTxt.text = GameManager.instance.skinData.skinList[skinIndex].price.ToString();
        }

        if(GameManager.instance.skinData.skinList[skinIndex].isUnlock)
        {
            tryBtn.SetActive(false);
            buyBtn.SetActive(false);
            getBtn.SetActive(false);

            GameManager.instance.selectPlayerSkin = skinIndex;
            GameManager.instance.mainBall.InitSkin(skinIndex);
            PlayerPrefs.SetInt("CurrentSelectChar", skinIndex);
        }
    }

    public void LeftCharacter()
    {
        if (skinIndex > 0)
            skinIndex--;
        else
            skinIndex = 9;
        RefreshCharacter();
    }

    public void RightCharacter()
    {
        if (skinIndex < 9)
            skinIndex++;
        else
            skinIndex = 0;
        RefreshCharacter();
    }

    public void TryChar()
    {
        GameManager.instance.skinData.skinList[skinIndex].isUnlock = true;
        RefreshCharacter();
    }

    public void TryCharRW()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.TRY_CHARACTER);
    }

    public void GetChar()
    {
        GameManager.instance.skinData.UnlockSkin(skinIndex);
        GameManager.instance.skinData.RefreshData();
        RefreshCharacter();
    }

    public void GetCharRW()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.GET_CHARACTER);
    }

    public void BuyChar()
    {
        if(GameManager.instance.coin >= (GameManager.instance.skinData.skinList[skinIndex].price))
        {
            GameManager.instance.coin -= GameManager.instance.skinData.skinList[skinIndex].price;
            GameManager.instance.uiManager.victoryPanel.UpdateCoinPanel();
            GameManager.instance.uiManager.gamePanel.InitUI();
            GameManager.instance.SaveData();
            GameManager.instance.skinData.UnlockSkin(skinIndex);
            GameManager.instance.skinData.RefreshData();
            RefreshCharacter();
        }
       
    }

    public void ShowVictory()
    {
        GameManager.instance.skinData.RefreshData();
        RefreshCharacter();

        skeletonGraphic.startingAnimation = "laugh";
        skeletonGraphic.Initialize(true);

        homeTitle.SetActive(false);
        victoryTitle.SetActive(true);
        failTitle.SetActive(false);
        coinPanel.SetActive(true);

        levelTxt.text = "LEVEL " + GameManager.instance.levelIndex;
        UpdateCoinPanel();
        GameManager.instance.SaveData();

        AdsControl.Instance.ShowInterstitalRandom();
    }

   

    public void ShowHome()
    {
        skinIndex = PlayerPrefs.GetInt("CurrentSelectChar");
        GameManager.instance.skinData.RefreshData();
        RefreshCharacter();

        skeletonGraphic.startingAnimation = "laugh";
        skeletonGraphic.Initialize(true);

        homeTitle.SetActive(true);
        victoryTitle.SetActive(false);
        failTitle.SetActive(false);
        coinPanel.SetActive(true);

        levelTxt.text = "LEVEL " + GameManager.instance.levelIndex;
        UpdateCoinPanel();
    }

    public void ShowFail()
    {
        GameManager.instance.skinData.RefreshData();
        RefreshCharacter();

        skeletonGraphic.startingAnimation = "hurt2";
        skeletonGraphic.Initialize(true);

        homeTitle.SetActive(false);
        victoryTitle.SetActive(false);
        failTitle.SetActive(true);
        coinPanel.SetActive(true);

        levelTxt.text = "LEVEL " + GameManager.instance.levelIndex;
        UpdateCoinPanel();
        GameManager.instance.SaveData();
        AdsControl.Instance.ShowInterstitalRandom();
    }

    public void HidePanel()
    {
        uiChild.SetActive(false);
        coinPanel.SetActive(false);
    }

    public void UpdateCoinPanel()
    {
        coinTxt.text = GameManager.instance.coin.ToString();
        lifeTxt.text = GameManager.instance.life.ToString();
    }

    public void StartToPlay()
    {
      //  if (AdsControl.Instance != null)
      //      AdsControl.Instance.DestroyBannerAd();
        StartCoroutine(ShowMask());
        
    }

    IEnumerator ShowMask()
    {
        
        HidePanel();
        mask.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        mask.SetActive(false);
        GameManager.instance.currentState = GameManager.STATE.PLAYING;
        
    }

    public void Replay()
    {
        GameManager.instance.ReplayScene();
    }

    public void NextLevel()
    {
        GameManager.instance.NextScene();
    }

    public void GetBonusLifeRW()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.BONUS_LIFE);
    }

    public void GetBonusCoinRW()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.BONUS_COIN);
    }

    public void Rate()
    {
        Application.OpenURL(gameUrl);
    }

    public void Follow()
    {
        Application.OpenURL(pageUrl);
    }
}
