using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazer : EnemyController
{
    protected override void Start()
    {
        if (FindObjectOfType<PlayerMove>())
        {
            target = FindObjectOfType<PlayerMove>().transform;
        }
        base.Start();
    }

    protected override void Attack()
    {
        AttackPos = transform.position + (target.position - transform.position).normalized;
        atkcd = attackcooldown;
        anim.SetTrigger("attack");
    }

    public override void SpawnDamage()
    {
        Instantiate(attack, AttackPos, Quaternion.LookRotation(Vector3.forward, target.position - transform.position) * Quaternion.Euler(0,0, Random.Range(-5,5) * 5));
    }
}
