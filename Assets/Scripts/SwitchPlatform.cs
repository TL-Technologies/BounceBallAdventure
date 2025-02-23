using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : MonoBehaviour
{
    public Transform mSwitch;

    public List<Transform> movingPlatformList;

    public List<Vector3> startPointList = new List<Vector3>();

    public List<Transform> endPointList;

    public LayerMask playerMask;

    public enum STATE
    {
        START,
        MOVE_DOWN,
        END,
        MOVE_UP
    }

    public STATE currentState;

    public float moveSpeed;

    private void Awake()
    {
        currentState = STATE.START;

        LoadStartPoint();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAngle();
        switch (currentState)
        {
            case STATE.START:

                break;

            case STATE.MOVE_DOWN:

                for(int i = 0; i < movingPlatformList.Count; i++)

                movingPlatformList[i].localPosition = Vector2.MoveTowards(startPointList[i], endPointList[i].localPosition, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(movingPlatformList[0].localPosition, endPointList[0].localPosition) <= 0.2f)
                {
                    currentState = STATE.END;
                }

                break;

            case STATE.END:

                break;

            case STATE.MOVE_UP:

              

                for (int i = 0; i < movingPlatformList.Count; i++)
                    movingPlatformList[i].localPosition = Vector2.MoveTowards(endPointList[i].localPosition, startPointList[i], moveSpeed * Time.deltaTime);

                if (Vector2.Distance(movingPlatformList[0].localPosition, startPointList[0]) <= 0.2f)
                {
                    currentState = STATE.START;
                }

                break;
        }
    }

    void LoadStartPoint()
    {
        for(int i =0; i < movingPlatformList.Count; i++)
        {
            startPointList.Add(movingPlatformList[i].localPosition);
        }
    }

    public void MoveDown()
    {
        if (currentState == STATE.START)
        {
            AudioManager.instance.pushBoxSfx.Play();
            currentState = STATE.MOVE_DOWN;
           
        }

    }

    public void MoveUp()
    {
        if (currentState == STATE.END)
        {
            AudioManager.instance.pushBoxSfx.Play();
            currentState = STATE.MOVE_UP;
            
        }

    }

    void CheckAngle()
    {
        float switchAngle = mSwitch.localRotation.z;

        if (switchAngle >= 0.3f)
        {


            //MoveDown();
            for (int i = 0; i < movingPlatformList.Count; i++)
                movingPlatformList[i].gameObject.SetActive(false);


        }
        else if(switchAngle <= -0.3f)
        {
            // MoveUp();
            for (int i = 0; i < movingPlatformList.Count; i++)
                movingPlatformList[i].gameObject.SetActive(true);
        }
    }
}
