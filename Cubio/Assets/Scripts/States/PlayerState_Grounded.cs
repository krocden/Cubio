using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Grounded : PlayerState
{
    //protected Vector2 velocity;   
    protected float velocityH;
    protected float velocityV;
    protected float movementDir;
    protected string input;
    protected float waitTime;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rigidbody;
    protected BoxCollider2D boxCollider;
    //protected Rigidbody2D rigidbody; 
    public PlayerState_Grounded(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){

    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        rigidbody = player.RB;
        sprite = player.Sprite;
        boxCollider = player.BC;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.buttonPressed;
        movementDir = player.InputHandler.movementDir;
        //rigidbody = player.RB;
        //isGrounded();

        if(input == "Z"){
            stateMachine.ChangeState(player.AttackState);
        } else if(input == "X"){
            stateMachine.ChangeState(player.AttackState);
        }
        if(input == "LEFT"){
            velocityH = movementDir * player.movementSpeed;
            sprite.flipX = true;
        }
        else if(input == "RIGHT"){
            velocityH = movementDir * player.movementSpeed;
            sprite.flipX = false;
        }
        if(input == "DUCK" && isGrounded()){
            stateMachine.ChangeState(player.DuckState);
        }
        if(input == "JUMP" && isGrounded()){
            jump();
            //Debug.Log("BRUH "+velocityV);
            stateMachine.ChangeState(player.AirState);
            velocityV = rigidbody.velocity.y;
        }else{
            velocityV = 0f;
            //Debug.Log("BRUH "+velocityV+ input);
            //input = null;
        }
        if(input == null){
            velocityH = 0;
            velocityV = 0;
        }
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    bool isGrounded(){ 
        float extraHeight = 0.1f;

        Vector3 LeftBottomCorner = boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y);
        Vector3 RightBottomCorner = boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, extraHeight - boxCollider.bounds.extents.y);
        
        Collider2D raycastHit = Physics2D.OverlapArea(LeftBottomCorner, RightBottomCorner, player.platformLayerMask);
        Color rayColor;
        if(raycastHit != null){    
            rayColor = Color.yellow;
        } else {
            rayColor = Color.magenta;
        }
        Debug.DrawLine(LeftBottomCorner,RightBottomCorner, rayColor);
        return raycastHit != null;
    }
    void jump(){
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, player.jumpForce);
    }
    protected void SetVelocityX(){
        //workspace.Set(velocity, CurrentVelocity.y);
        //rigidbody.velocity = workspace;
        //CurrentVelocity = workspace;
        rigidbody.velocity = new Vector2(movementDir * player.movementSpeed, rigidbody.velocity.y);
    }
     
}
