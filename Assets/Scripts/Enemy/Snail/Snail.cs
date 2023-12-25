using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new SnailPatrolState();
        chaseState = new SnailChaseState();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isDie)
        {
            anim.SetBool("rollIn", false);
        }
    }
}
