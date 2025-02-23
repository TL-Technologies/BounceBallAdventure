using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float moveSpeed;

    public float currentSpeed;

    public float xSize;

    public LayerMask wallMask;

    public GameObject deathFx;

    public void InitSkin()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentSpeed = - moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(currentSpeed, rigidBody.velocity.y);

        if (CheckLeftSide())
        {
            
            currentSpeed = moveSpeed;
        }
            

        if (CheckRightSide())
            currentSpeed = -moveSpeed;
    }

    public bool CheckLeftSide()
    {
        bool _check = false;

        _check = Physics2D.OverlapCircle(new Vector2(transform.position.x - xSize, transform.position.y), 0.1f, wallMask);

        return _check;
    }

    public bool CheckRightSide()
    {
        bool _check = false;

        _check = Physics2D.OverlapCircle(new Vector2(transform.position.x + xSize, transform.position.y), 0.1f, wallMask);

        return _check;
    }

    public void Die()
    {
        Instantiate(deathFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
          
            BallController _player = collision.gameObject.GetComponent<BallController>();
            _player.Hurt();
        }
    }
}
