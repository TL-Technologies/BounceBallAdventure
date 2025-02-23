using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FriendBox : MonoBehaviour
{
    private Animator mAnimator;

    public GameObject helpObj;

    public SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rescue()
    {
        mAnimator.SetTrigger("Action");
        AudioManager.instance.breakBoxSfx.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        helpObj.SetActive(false);
        skeletonAnimation.state.SetAnimation(0, "laugh", true);
    }
}
