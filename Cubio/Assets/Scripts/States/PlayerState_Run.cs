using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Run : PlayerState_Grounded
{
    //private Vector2 workspace;
    //Vector2 CurrentVelocity;
    
    public PlayerState_Run(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){

    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        //rigidbody = player.RB;
        //player.SetVelocityX(0f);
        //Debug.Log("AM RUNNING");
    }
    public override void Exit()
    {
        base.Exit();
       //Debug.Log("AM NOT RUNNING ANYMORE");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SetVelocityX();
        //anim.SetBool("isRunning", true);
        //Debug.Log("Current VEL H: "+velocityH);
        if(velocityH == 0f){
            //Debug.Log("TO IDLE "+velocityH);
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    // void SetVelocityX(){
    //     //workspace.Set(velocity, CurrentVelocity.y);
    //     //rigidbody.velocity = workspace;
    //     //CurrentVelocity = workspace;
    //     rigidbody.velocity = new Vector2(movementDir * player.movementSpeed, rigidbody.velocity.y);
    // }    
}
