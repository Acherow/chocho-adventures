using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyController
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
        if(target)
        {
            targetPoint = target.position;
        }
    }
}
