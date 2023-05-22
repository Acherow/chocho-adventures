using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : EnemyController
{
    protected override void Start()
    {
        if (FindObjectOfType<PlayerMove>())
        {
            target = FindObjectOfType<PlayerMove>().transform;
        }
        base.Start();
    }

    public void Update()
    {
        if (target)
        {
            targetPoint = target.position + ((transform.position - target.position).normalized * range * 0.9f);
        }
    }

    protected override void Attack()
    {
        AttackPos = transform.position + (target.position - transform.position).normalized;
        atkcd = attackcooldown;
        anim.SetTrigger("attack");
    }

    public override void SpawnDamage()
    {
        Instantiate(attack, AttackPos, Quaternion.LookRotation(Vector3.forward, target.position - transform.position));
    }
}
