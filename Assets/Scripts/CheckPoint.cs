using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public SpriteRenderer _sprite;

    public Sprite activeSpr;

    public Animator _animator;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
            return;

        if(collision.gameObject.tag == "Player")
        {
            isActive = true;
            AudioManager.instance.checkPointSfx.Play();
            GameManager.instance.checkPointList.Add(transform.position);
            _animator.enabled = true;
            StartCoroutine(ActiveSpr());
        }
    }

    IEnumerator ActiveSpr()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.enabled = false;
        _sprite.sprite = activeSpr;
    }
}
