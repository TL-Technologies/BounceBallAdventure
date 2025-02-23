using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButtonPlatform : MonoBehaviour
{
    public Transform objRotate1,objRotate2, baseObj;

    public Switch mSwitch;

    public float maxSpeed;

    public float currentSpeed;

    public LayerMask playerMask;

    public enum STATE
    {
        START,
        MOVE_DOWN,
        END,
        MOVE_UP
    }

    public STATE currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.START;
        currentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();

        switch (currentState)
        {
            case STATE.START:

                break;

            case STATE.MOVE_DOWN:

                objRotate1.RotateAroundLocal(Vector3.back, currentSpeed * Time.deltaTime);

                objRotate2.RotateAroundLocal(Vector3.forward, currentSpeed * Time.deltaTime);

                if (objRotate1.localRotation.z <= 0.01f)
                {
                    currentSpeed = 0.0f;
                    currentState = STATE.END;
                }
                   

                break;

            case STATE.END:

                break;

            case STATE.MOVE_UP:

                objRotate1.RotateAroundLocal(Vector3.forward, currentSpeed * Time.deltaTime);

                objRotate2.RotateAroundLocal(Vector3.back, currentSpeed * Time.deltaTime);

                if (objRotate1.localRotation.z >= 0.71f)
                {
                    currentSpeed = 0.0f;
                    currentState = STATE.START;
                }
                   



                break;
        }
    }

    public void MoveDown()
    {
        if (currentState == STATE.START)
        {
            currentSpeed = maxSpeed;
            AudioManager.instance.pushBoxSfx.Play();
            currentState = STATE.MOVE_DOWN;
            mSwitch.GetComponent<Animator>().SetInteger("press", 1);
        }

    }

    public void MoveUp()
    {
        if (currentState == STATE.END)
        {
            currentSpeed = maxSpeed;
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
