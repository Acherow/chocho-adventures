using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWand : MonoBehaviour
{
    public Transform pivot;
    float distance = 1.6f;
    public GameObject bulletPrefab;
    public float range;
    public float cooldown;
    float cd;
    public LayerMask targets;

    Transform misnery;

    public PlayerStats stats;
    PlayerMove mov;

    private void Start()
    {
        mov = GetComponent<PlayerMove>();
        stats = RunManager.CurrentRun;

        StartCoroutine(CheckClosest());        
    }

    private void FixedUpdate()
    {
        if (mov.animFrozen)
            return;

        if (misnery != null)
        {
            //pivot.right = misnery.position - transform.position;
            Vector2 dir = (misnery.position + (Vector3)misnery.GetComponent<Rigidbody2D>().velocity.normalized * Vector2.Distance(misnery.position, transform.position) * 0.5f) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pivot.rotation = Quaternion.Slerp(pivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10f * Time.deltaTime);
            if (Vector2.Distance(misnery.position, transform.position) > range)
                misnery = null;
        }

        if (misnery != null && cd <= 0)
        {
            Shoot();
        }
        if (cd > 0)
            cd -= Time.deltaTime;
    }

    IEnumerator CheckClosest()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(pivot.position, range, targets);
        if (enemies.Length > 0)
        {
            misnery = enemies[0].transform;
            foreach (var en in enemies)
            {
                if (en.GetComponent<Rigidbody2D>() && Vector2.Distance(transform.position, en.transform.position) < Vector2.Distance(transform.position, misnery.transform.position))
                    misnery = en.transform;
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckClosest());
    }

    void Shoot()
    {
        cooldown = stats.FireSpeed;
        StartCoroutine(DecideShots());
        cd = cooldown;
    }

    IEnumerator DecideShots()
    {
        Vector2 pos = (pivot.position + (pivot.right * distance));
        Quaternion rot = pivot.rotation * Quaternion.Euler(0, 0, -90);

        if (stats.DoubleShot <= 0 && stats.TripleShot <= 0)
        {
            SpawnShot(pos, rot);
        }
        else
        {
            for (int i = 0; i < Mathf.Max(stats.DoubleShot, stats.TripleShot); i++)
            {
                if (i < stats.DoubleShot)
                {
                    SpawnShot((Vector3)pos + (Quaternion.AngleAxis(90, Vector3.forward) * (pos - (Vector2)transform.position) * 0.2f), rot);
                    SpawnShot((Vector3)pos + (Quaternion.AngleAxis(-90, Vector3.forward) * (pos - (Vector2)transform.position) * 0.2f), rot);
                }
                if (i < stats.TripleShot)
                {
                    SpawnShot(pos, rot * Quaternion.Euler(0,0,30));
                    SpawnShot(pos, rot);
                    SpawnShot(pos, rot * Quaternion.Euler(0, 0, -30));
                }
                yield return new WaitForSeconds(cooldown / Mathf.Max(stats.DoubleShot, stats.TripleShot));
            }
        }
    }

    void SpawnShot(Vector2 position, Quaternion rotation)
    {
        Shot s = Instantiate(bulletPrefab, position, rotation).GetComponent<Shot>();
        s.bulletstats = stats.Clone();
    }
}