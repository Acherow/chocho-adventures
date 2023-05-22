using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool frozen;

    public Vector2 targetPoint;
    public float speed;

    public GameObject attack;
    public float range;
    public float attackcooldown;
    protected float atkcd;
    protected Rigidbody2D rb;
    protected Animator anim;
    SpriteRenderer sr;

    protected Transform target;

    bool facingRight;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        if (atkcd > 0)
            atkcd -= Time.deltaTime;

        if (target)
        {
            if (Vector2.Distance(transform.position, targetPoint) > 0.1f && !frozen)
            {
                rb.AddForce((targetPoint - (Vector2)transform.position).normalized * speed);
            }

            if (Vector2.Distance(transform.position, target.position) <= range && atkcd <= 0)
                Attack();
        }

        HandleAnim();
    }

    protected Vector2 AttackPos;
    protected virtual void Attack()
    {
        AttackPos = target.position;
        atkcd = attackcooldown;
        anim.SetTrigger("attack");
    }

    public virtual void SpawnDamage()
    {
        Instantiate(attack, AttackPos, Quaternion.identity);
    }

    protected virtual void HandleAnim()
    {
        if ((facingRight && rb.velocity.x < 0) || (!facingRight && rb.velocity.x > 0))
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
            sr.flipX = !sr.flipX;
            facingRight = !facingRight;
        }
        anim.SetBool("moving", rb.velocity.magnitude > 0.1f);
    }
}
