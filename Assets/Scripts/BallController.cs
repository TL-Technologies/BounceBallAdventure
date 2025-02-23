using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
using Spine.Unity;

public class BallController : MonoBehaviour
{
    public float moveSpeed;

    private float moveSpeedControl;

    public bool isMobile;

    private Rigidbody2D rigidBody;

    public float jumpForce;

    public float ballRadius, checkGroundRadius;

    public bool isGround, hitSpring;

    public LayerMask groundMask, physicObjMask, enemiesMask;

    public Animator _animator;

    public Transform centerTransform;

    public ParticleSystem dustFx;

    private bool isFreezing;

    public GameObject deathFx;

    public SkeletonMecanim skeletonMecanim;
   

    private void Awake()
    {
        Application.targetFrameRate = 60;
       
    }

    public void InitSkin(int currentSkin)
    {
        skeletonMecanim.initialSkinName = "skin_0" + currentSkin ;
        skeletonMecanim.Initialize(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isFreezing = false;
        moveSpeedControl = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFreezing)
            return;
        if (GameManager.instance.currentState != GameManager.STATE.PLAYING)
            return;
        float movement = 0.0f;

        if (!isMobile)
            movement = Input.GetAxis("Horizontal") * moveSpeed;
        else
            movement = moveSpeed * moveSpeedControl;

        rigidBody.velocity = new Vector2(movement, rigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        CheckGround();
        CheckEnemies();
        CheckPhysicObj();

    }

    public void Move(float _direction)
    {
        moveSpeedControl = _direction;
    }

    public void StopMove()
    {
        moveSpeedControl = 0.0f;
    }

    public void Jump()
    {
        if (isGround)
        {
            AudioManager.instance.jumpSfx.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }         
    }

    public void Bounce()
    {
        if (isGround)
        {
            AudioManager.instance.bounceSfx.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * 1.5f);
        }
    }

    void CheckGround()
    {
        
        isGround = Physics2D.OverlapCircle(new Vector2(centerTransform.position.x, centerTransform.position.y - ballRadius), checkGroundRadius, groundMask);
        _animator.SetBool("IsGrounded", isGround);
    }

    void CheckPhysicObj()
    {
        Collider2D hitPhysic = Physics2D.OverlapCircle(new Vector2(centerTransform.position.x, centerTransform.position.y - ballRadius), checkGroundRadius, physicObjMask);

        if (hitPhysic)
        {


            if (hitPhysic.gameObject.tag == "Friend")
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                AudioManager.instance.yeeSfx.Play();

                hitPhysic.gameObject.GetComponent<FriendBox>().Rescue();
            }

        }
    }

    void CheckEnemies()
    {
        Collider2D hitEnemyCenter = Physics2D.OverlapCircle(new Vector2(centerTransform.position.x, centerTransform.position.y - ballRadius - 0.2f), checkGroundRadius, enemiesMask);

        if(hitEnemyCenter)
        {
            

            if (hitEnemyCenter.GetComponent<RollingEnemy>() != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                AudioManager.instance.yeeSfx.Play();
                hitEnemyCenter.GetComponent<RollingEnemy>().Die();
            }
              
        }

        Collider2D hitEnemyLeft = Physics2D.OverlapCircle(new Vector2(centerTransform.position.x - ballRadius * 0.5f, centerTransform.position.y - ballRadius - 0.2f), checkGroundRadius, enemiesMask);

        if (hitEnemyLeft)
        {


            if (hitEnemyLeft.GetComponent<RollingEnemy>() != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                AudioManager.instance.yeeSfx.Play();
                hitEnemyLeft.GetComponent<RollingEnemy>().Die();
            }

        }

        Collider2D hitEnemyRight = Physics2D.OverlapCircle(new Vector2(centerTransform.position.x + ballRadius * 0.5f, centerTransform.position.y - ballRadius - 0.2f), checkGroundRadius, enemiesMask);

        if (hitEnemyRight)
        {


            if (hitEnemyRight.GetComponent<RollingEnemy>() != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                AudioManager.instance.yeeSfx.Play();
                hitEnemyRight.GetComponent<RollingEnemy>().Die();
            }

        }
    }

  

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            dustFx.transform.position = new Vector2(centerTransform.position.x, centerTransform.position.y - ballRadius);
            dustFx.Play();
        }

        
    }

  

    public void Freeze(Vector3 pos)
    {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rigidBody.gravityScale = 0.0f;
        rigidBody.velocity = Vector2.zero;
        rigidBody.freezeRotation = true;
        isFreezing = true;
        _animator.SetBool("Finish", true);
        Scale();
    }

    public void Scale()
    {
        Sequence s = DOTween.Sequence();
        ///s.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f).SetRelative().SetEase(Ease.Linear));
        s.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f).SetEase(Ease.Linear));

        s.OnComplete(() =>
        {
            Sequence s1 = DOTween.Sequence();
            s1.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1.0f).SetEase(Ease.Linear));

            s1.OnComplete(() =>
            {
                Sequence s2 = DOTween.Sequence();
                s2.Append(transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 0.25f).SetEase(Ease.Linear));

                s2.OnComplete(() =>
                {
                    GameManager.instance.uiManager.ShowVictoryPanel();
                    GameManager.instance.uiManager.victoryPanel.ShowVictory();

                });
            });
        });

    }

    public void Hurt()
    {
        rigidBody.velocity = new Vector2(0, jumpForce * 1.5f);
        AudioManager.instance.hurtSfx.Play();
        _animator.SetBool("Hurt", true);
        isFreezing = true;
        StartCoroutine(Die());
    }

    public void HitWater()
    {
        rigidBody.velocity = new Vector2(0, - jumpForce);
        AudioManager.instance.hurtSfx.Play();
        _animator.SetBool("Hurt", true);
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(deathFx, transform.position, transform.rotation);
        _animator.SetBool("Hurt", false);

        PauseTheBall();

        if (GameManager.instance.life > 0)
        {
            GameManager.instance.life--;
            GameManager.instance.uiManager.victoryPanel.UpdateCoinPanel();
            GameManager.instance.uiManager.gamePanel.InitUI();
           
        }

        if (GameManager.instance.life == 0)
        {
            StartCoroutine(ShowRetriveIE());
            
        }

        else
        {
            GameManager.instance.Respawn();
        }

        
    }

    IEnumerator ShowRetriveIE()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.uiManager.ShowRetrivePanel();
    }

    IEnumerator ShowGameOverIE()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.uiManager.ShowVictoryPanel();
        GameManager.instance.uiManager.victoryPanel.ShowFail();
    }

    public void PauseTheBall()
    {
        isFreezing = true;
        rigidBody.gravityScale = 0.0f;
        rigidBody.velocity = Vector2.zero;
        rigidBody.freezeRotation = true;
        GetComponent<Collider2D>().enabled = false;
    }

    public void ResumeTheBall()
    {
        rigidBody.gravityScale = 5.0f;
        rigidBody.freezeRotation = false;
        GetComponent<Collider2D>().enabled = true;
        isFreezing = false;
    }

}
