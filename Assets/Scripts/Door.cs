using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator mAnimator;

    private BallController ball;

    public enum STATE
    {
        CLOSE,
        OPEN,
        FINISH
    }

    public STATE currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.CLOSE;
        mAnimator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case STATE.CLOSE:

                break;

            case STATE.OPEN:

                if (Vector3.Distance(transform.position, ball.transform.position) <= 2.0f)
                {
                    ball.Freeze(transform.position);
                    AudioManager.instance.yahooSfx.Play();
                    currentState = STATE.FINISH;
                }
                  

                break;

            case STATE.FINISH:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mAnimator.SetBool("OpenDoor", true);
            currentState = STATE.OPEN;
            ball = collision.GetComponent<BallController>();
            AudioManager.instance.openDoorSfx.Play();
        }
    }
}
