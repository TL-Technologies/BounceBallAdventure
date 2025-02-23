using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCrate : MonoBehaviour
{
    private Rigidbody2D mRigidbody;

    private bool isDrop;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        isDrop = false;
    }
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(0.5f);
        mRigidbody.bodyType = RigidbodyType2D.Dynamic;
          
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDrop)
        {
            isDrop = true;
            StartCoroutine(Drop());
        }
    }
}
