using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinData : MonoBehaviour
{
    public List<Skin> skinList = new List<Skin>();

    public void RefreshData()
    {
        for(int i = 0; i < skinList.Count; i++)
        {
            if (skinList[i].currentType == Skin.TYPE.UNLOCKED)
                skinList[i].isUnlock = true;
            else
            {
                int isSkinUnlock = PlayerPrefs.GetInt("UnlockSkin" + skinList[i].skinIndex);

                if(isSkinUnlock == 1)
                {
                    skinList[i].isUnlock = true;
                }
                else
                    skinList[i].isUnlock = false;
            }
        }
    }

    public void UnlockSkin(int _index)
    {
        PlayerPrefs.SetInt("UnlockSkin" + _index, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]

    public class Skin
    {
        public int skinIndex;

        public enum TYPE
        {
            UNLOCKED,
            TRY_BY_RW,
            BUY_BY_COIN,
            GET_BY_RW
        }

        public TYPE currentType;

        public bool isUnlock;

        public int price;
    }
}
