using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Duck : PlayerState
{
    string input;

    int playerLayer, platformLayer;
    BoxCollider2D boxCollider;
    public PlayerState_Duck(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        anim.Play("Duck");
        boxCollider = player.BC;
        changeCollider("Enter");
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platforms");
    }
    public override void Exit()
    {
        base.Exit();
        changeCollider("Exit");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.buttonPressed;
        if(input == "JUMP"){
            Debug.Log("JUMPING");
            if(platformBelow()){
                player.StartCoroutine(jumpOffPlatform());
            }
        }else if(input != "DUCK"){
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void changeCollider(string change){
        if(change == "Enter"){
            boxCollider.size = new Vector2(1.5f, 0.55f);
            boxCollider.offset = new Vector2(0f, -0.5f);
        } 
        else if(change == "Exit"){
            boxCollider.size = new Vector2(0.5f, 1);
            boxCollider.offset = new Vector2(0, -0.25f);
        }
    }
    IEnumerator jumpOffPlatform(){
        Debug.Log("REE");
        //jumpingOff = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        yield return new WaitForSeconds(0.13f);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        //jumpingOff = false;
    }

    bool platformBelow(){
        Vector3 platformOnScreen = new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (boxCollider.bounds.size.y), boxCollider.bounds.center.z);
        RaycastHit2D platformBelow = Physics2D.Raycast(platformOnScreen, Vector2.down, Screen.height, player.platformLayerMask);
        Color rayColor;
        if (platformBelow.collider != null){
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        //Debug.DrawRay(platformOnScreen, Vector2.down * Screen.height, Color.magenta, 2f);
        //Debug.Log("PLATFORM BELOW : "+platformBelow.collider);
        return platformBelow.collider != null;
    }
    
}
