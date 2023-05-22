using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    public Transform pl;
    public List<Vector3> poss;

    public float timer;

    public Vector2 dir;
    bool facingRight;
    Animator anim;
    Rigidbody2D rb;

    LineRenderer rend;

    public bool freehit;

    private void Start()
    {
        facingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rend = GetComponentInChildren<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (poss.Count <= 0)
            timer = 2f;
        poss.Add(pl.position);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Color c = sr.color;
            c.a = 0;
            sr.color = c;
        }

        if (timer < 0)
        {
            Color c = sr.color;
            c.a = .5f;
            sr.color = c;

            freehit = true;

            poss.RemoveAt(0);

            HandleVisuals();
            dir = (poss[0] - transform.position);
            if (dir.magnitude > 0.1f)
                rb.AddForce(dir.normalized * 50);
        }

        rend.positionCount = poss.Count;
        rend.SetPositions(poss.ToArray());
    }

    public void Activate()
    {
        if (timer < 0)
        {
            timer = 2f;
            StartCoroutine(TravelBack());
        }
    }

    IEnumerator TravelBack()
    {
        pl.GetComponent<PlayerMove>().animFrozen = true;
        while(poss.Count > 0)
        {
            yield return new WaitForEndOfFrame();
            pl.position = poss[^1];
            poss.RemoveAt(poss.Count - 1);
        }
        pl.GetComponent<PlayerMove>().animFrozen = false;
        timer = 2f;
        freehit = false;
    }

    SpriteRenderer sr;
    void HandleVisuals()
    {
        if ((facingRight && dir.x < 0) || (!facingRight && dir.x > 0))
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
            sr.flipX = !sr.flipX;
            facingRight = !facingRight;
        }
        anim.SetBool("moving", dir.magnitude > 0.1f);
    }
}
