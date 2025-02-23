using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButtonPlatform : MonoBehaviour
{
    public Transform objMove,baseObj;

    public Transform startPoint, endPoint;

    public float moveSpeed;

    public Switch mSwitch;

    public LayerMask playerMask;

    public enum STATE
    {
        START,
        MOVE_DOWN,
        END,
        MOVE_UP
    }

    public STATE currentState;

    private void Awake()
    {
        objMove.position = startPoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.START;   
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();

        switch(currentState)
        {
            case STATE.START:

                break;

            case STATE.MOVE_DOWN:

                objMove.position = Vector2.MoveTowards(objMove.position, endPoint.position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(objMove.position, endPoint.position) <= 0.2f)
                {
                    currentState = STATE.END;
                }

                break;

            case STATE.END:

                break;

            case STATE.MOVE_UP:

                objMove.position = Vector2.MoveTowards(objMove.position, startPoint.position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(objMove.position, startPoint.position) <= 0.2f)
                {
                    currentState = STATE.START;
                }

                break;
        }
    }

    public void MoveDown()
    {
        if(currentState == STATE.START)
        {
            AudioManager.instance.pushBoxSfx.Play();
            currentState = STATE.MOVE_DOWN;
            mSwitch.GetComponent<Animator>().SetInteger("press", 1);
        }
        
    }

    public void MoveUp()
    {
        if (currentState == STATE.END)
        {
            AudioManager.instance.pushBoxSfx.Play();
            currentState = STATE.MOVE_UP;
            mSwitch.GetComponent<Animator>().SetInteger("press", -1);
        }

    }

    void CheckPlayer()
    {
        Collider2D hitPhysic = Physics2D.OverlapCircle(new Vector2(baseObj.position.x, baseObj.position.y + 2.0f), 0.1f, playerMask);

        if (hitPhysic)
        {

            if (hitPhysic.gameObject.tag == "Player" || hitPhysic.gameObject.tag == "Box")
            {
                MoveDown();
            }

        }
        else
        {
            MoveUp();
        }
    }

}
