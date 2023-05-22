using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Vector2 dir;
    Animator anim;
    Rigidbody2D rb;

    public bool animFrozen;

    bool facingRight;

    private void Start()
    {
        facingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        HandleVisuals();
        if (!animFrozen)
            rb.AddForce(dir * speed);
        else
            rb.velocity = Vector2.zero;
    }

    SpriteRenderer sr;
    void HandleVisuals()
    {
        if((facingRight && dir.x < 0) || (!facingRight && dir.x > 0))
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
            sr.flipX = !sr.flipX;
            facingRight = !facingRight;
        }
        anim.SetBool("moving", dir != Vector2.zero);
    }
}
