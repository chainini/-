using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBeePatrolState : BaseState
{
    private Vector3 Pos;
    private float wait;
    private float waitCounter;

    public override void OnEnter(Enemy enemy)
    {
        wait = 2f;
        waitCounter=wait;

        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        Pos = new Vector3(Random.Range(-12,12),Random.Range(-2,8),0);
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        if (currentEnemy.transform.position.x - EventHandle.CallPlayerPos().x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.transform.position!= Pos)
        {
            currentEnemy.anim.SetBool("fly", true);
            currentEnemy.transform.position=Vector3.MoveTowards(currentEnemy.transform.position, Pos, 2*Time.deltaTime);
        }
        if(currentEnemy.transform.position==Pos)
        {
            WaitTime();
        }
    }

    private void WaitTime()
    {
        waitCounter-=Time.deltaTime;
        if(waitCounter<=0)
        {
            Attack();
            Pos = EventHandle.CallSmallBeeRandomPos();
            waitCounter = wait;
        }
    }

    public void Attack()
    {
        currentEnemy.anim.SetTrigger("attack");
    }


    
    public override void OnExit()
    {
        
    }
}
