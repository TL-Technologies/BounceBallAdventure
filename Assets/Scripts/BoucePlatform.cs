using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucePlatform : MonoBehaviour
{
    public LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        Collider2D hitPhysic = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 2.0f), 0.1f, playerMask);

        if (hitPhysic)
        {

           if(hitPhysic.gameObject.tag == "Player")
            {
                hitPhysic.gameObject.GetComponent<BallController>().Bounce();
            }

        }
        
    }
}
