using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float cooldown;

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else
            GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(cooldown <= 0 && collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().TakeAHit();
            GetComponent<Collider2D>().enabled = false;
            cooldown = 1;
        }
    }
}
