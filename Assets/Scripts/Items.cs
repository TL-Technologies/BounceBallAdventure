using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject itemFx;

    public int valueToAdd;

    public enum ItemType
    {
        COIN, LIFE, KEY, MAGNET
    }


    public ItemType currentType;

    // Start is called before the first frame update
    void Start()
    {
        if (valueToAdd == 0)
            valueToAdd = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentType == ItemType.COIN)
        {
            if (!GameManager.instance.getMagenet)
                return;
            if(Vector3.Distance(transform.position, GameManager.instance.mainBall.transform.position) <= 10.0f)

                transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.mainBall.transform.position, 20.0f * Time.deltaTime);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           

            switch(currentType)
            {
                case ItemType.COIN:
                    GameManager.instance.AddCoin(valueToAdd);
                    break;

                case ItemType.LIFE:
                    GameManager.instance.uiManager.gamePanel.AddMoreLife(valueToAdd);
                    break;

                case ItemType.MAGNET:
                    ShowMagnetRW();
                    break;
            }

            AudioManager.instance.getCoinSfx.Play();
            Instantiate(itemFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void ShowMagnetRW()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.BONUS_MAGNET);
    }
}
