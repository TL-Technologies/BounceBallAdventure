using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float _direct)
    {
        GameManager.instance.mainBall.Move(_direct);
    }

    public void StopMove()
    {
        GameManager.instance.mainBall.StopMove();
    }

    public void Jump()
    {
        GameManager.instance.mainBall.Jump();
    }
}
