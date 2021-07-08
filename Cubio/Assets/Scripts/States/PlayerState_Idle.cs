using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerState_Grounded
{
    public PlayerState_Idle(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        anim.Play("Idle");
        Debug.Log("ENTER IDLE " + velocityV);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //anim.SetBool("isRunning", false);
        if(velocityH != 0f){
            stateMachine.ChangeState(player.RunState);
        }   
        // if(velocityV != 0f){
        //     Debug.Log("IDLE " + velocityV);
        //     stateMachine.ChangeState(player.AirState);
        // }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
