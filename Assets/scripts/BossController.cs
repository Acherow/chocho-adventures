using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject slimeAttack;
    public GameObject EyeAttack;
    public float idletimer;

    private void Start()
    {
        idletimer = 6;
    }

    private void Update()
    {
        idletimer -= Time.deltaTime;
        if(idletimer <= 0)
        {
            if (Random.Range(0, 2) == 1)
                Slime(); 
            else 
                Eye();
            idletimer = Random.Range(3,6);
        }
    }

    void Slime()
    {
        Instantiate(slimeAttack, transform.position + (Random.Range(0,2) == 1?Vector3.right*5:-Vector3.right*5), Quaternion.identity);
    }

    void Eye()
    {
        GameObject g = Instantiate(EyeAttack, transform.position, Quaternion.Euler(new Vector3(0,0,Random.Range(190,170))));
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), g.GetComponent<Collider2D>());
    }
}
