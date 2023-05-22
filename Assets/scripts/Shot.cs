using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Rigidbody2D rb;
    public PlayerStats bulletstats;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bulletstats.Lifetime -= Time.deltaTime;
        if (bulletstats.Lifetime <= 0)
            Destroy(gameObject);
        rb.AddForce(transform.up * bulletstats.Speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<health>())
        {
            bulletstats.Pierce--;
            col.gameObject.GetComponent<health>().TakeDamage(bulletstats.Damage);

            if(bulletstats.HasFire)
            {
                Fire f;
                if (col.gameObject.GetComponent<Fire>())
                    f = col.gameObject.GetComponent<Fire>();
                else
                    f = col.gameObject.AddComponent<Fire>();

                f.length = bulletstats.BurnTime;
                f.damage = bulletstats.BurnDamage;
            }
            if (bulletstats.HasLightning)
            {
                Lightning l = col.gameObject.AddComponent<Lightning>();
                l.damage = bulletstats.Damage;
                l.dmgreduction = bulletstats.ChainDamageReduction;
                l.count = bulletstats.ChainCount;
                l.ignoredmg = true;
                l.range = bulletstats.ChainRange;
            }
            if (bulletstats.HasIce)
            {
                Ice i = col.gameObject.AddComponent<Ice>();
                i.time = bulletstats.FreezeTime;
                i.amount = bulletstats.FreezeAmount;
            }
        }


        if (bulletstats.Pierce <= 0)
        {
            bulletstats.Lifetime = 0;
            return;
        }

        if (gameObject && col.gameObject && !col.isTrigger && bulletstats.Bounce > 0)
        {
            rb = GetComponent<Rigidbody2D>();
            Vector3 dir = Vector3.Reflect(rb.velocity, ((Vector2)transform.position - col.ClosestPoint(transform.position)).normalized);            
            rb.velocity = Vector2.zero;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
            bulletstats.Bounce--;
        }
    }
}
