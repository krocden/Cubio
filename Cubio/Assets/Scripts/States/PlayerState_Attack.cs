using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Attack : PlayerState
{
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;
    string input;
    LayerMask enemyLayer;
    float attackDelay;
    float startAttackDelay;
    //Transform attackPos;
    float attackRange;
    int skillDamage;
    public PlayerState_Attack(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        enemyLayer = LayerMask.GetMask("Enemies");
        boxCollider = player.BC;
        sprite = player.Sprite;
        //player.InputHandler.buttonPressed = null;
        //Debug.Log("ATTACK MODE");
        attackDelay = 0.2f;
    }
    public override void Exit()
    {
        base.Exit();
        attackDelay = 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.buttonPressed;
        //Debug.Log("INPUT:" + input);
        
        if(input == "Z"){
            //Debug.Log("INPUT Z");
            if(attackDelay <= 0){
                if(player.currentMana >= 200){
                    startAttackDelay = 0.425f;
                    spearCrusher();
                    attackDelay = startAttackDelay;
                } else {
                    Debug.Log("NOT ENOUGH MANA");
                    stateMachine.ChangeState(player.IdleState);
                }
                
            } else {
                attackDelay -= Time.deltaTime;
            }
            
        } else if (input == "X"){

        } else {
            //Debug.Log("Back to IDLE");
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void spearCrusher(){
        Debug.Log("SPEAR CRUSHING");
        anim.Play("Attack_Z");
        //player.StartCoroutine(startDelayCoroutine(1));
        attackRange = 2f;
        skillDamage = 400;
        Vector2 startPos;
        if(flipped()){
            float distanceFromBody = 2.5f;
            startPos = new Vector2(boxCollider.bounds.center.x - distanceFromBody, boxCollider.bounds.center.y);
        } else {
            float distanceFromBody = 2.5f;
            startPos = new Vector2(boxCollider.bounds.center.x + distanceFromBody, boxCollider.bounds.center.y);
        }
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(startPos, attackRange, enemyLayer);
        foreach(Collider2D enemy in enemiesHit){
            int damageSend = (skillDamage/100) * Random.Range(player.attackRangeLow, player.attackRangeHigh);
            enemy.GetComponent<Boss>().hitMonster(damageSend, player.critChance, player.critDamage);
            Debug.Log("SENT  \""+enemy.name+"\" "+ damageSend + " DAMAGE");
        }
        player.currentMana -= 750;
    }
    bool flipped(){
        // if(sprite.flipX == true){
        //     return true;
        // }
        // return false;
        return sprite.flipX != false;
    }

    IEnumerator startDelayCoroutine(int delaySec){
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(delaySec);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

   
    
}
