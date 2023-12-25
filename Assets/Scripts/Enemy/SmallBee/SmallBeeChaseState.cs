using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SmallBeeChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }
    public override void LogicUpdate()
    {
        if (Vector3.Distance(currentEnemy.transform.position,EventHandle.CallPlayerPos())>4f)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
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
        if(!currentEnemy.isHurt)
        {
            currentEnemy.rb.AddForce((EventHandle.CallPlayerPos() - currentEnemy.transform.position).normalized * 15, ForceMode2D.Impulse);
        }
        
    }
    public override void OnExit()
    {
        currentEnemy.rb.velocity=Vector2.zero;
    }
}
