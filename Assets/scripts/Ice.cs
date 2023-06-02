using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer rend;
    public float amount;
    public float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(rb.bodyType != RigidbodyType2D.Static)
            rb.velocity /= amount + 1;
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(this);
        rend.color = Color.cyan;
    }
}
