using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailChaseState : BaseState
{
    //public bool isRollIn;
    //public bool isRollOut;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //isRollIn = true;
        //isRollOut = false;
        currentEnemy.isRoll = true;
        currentEnemy.anim.SetBool("rollIn", true);
    }
    public override void LogicUpdate()
    {
        if (!currentEnemy.FoundPlayer())
        {
            
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        //if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        //{
        //    currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        //} 
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("rollOut", true);
        currentEnemy.anim.SetBool("rollIn", false);

        currentEnemy.isRoll = false;

        //isRollIn =false;
        //isRollOut = false;

        //currentEnemy.anim.SetBool("walk", true);
    }
}
