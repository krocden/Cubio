using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Air : PlayerState_Grounded
{
    public PlayerState_Air(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        anim.Play("Jump");
        player.InputHandler.buttonPressed = null;
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("0F EXIT " + velocityV);
        velocityV = 0f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //anim.SetBool("isRunning", false);
        SetVelocityX();
        velocityV = rigidbody.velocity.y;
        if(velocityV == 0f){
            Debug.Log("0F -> IDLE" + velocityV);
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
