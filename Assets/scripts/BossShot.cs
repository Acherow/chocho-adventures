using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float modifier;
    public GameObject SecondShot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, 0, Time.deltaTime * modifier);
        rb.AddForce(transform.up * speed);
        if (speed < 50f)
        {
            if(SecondShot)
            Instantiate(SecondShot, transform.position, Quaternion.Euler(new Vector3(0,0,Random.Range(0,90))));
            Destroy(gameObject);
        }
    }
}
