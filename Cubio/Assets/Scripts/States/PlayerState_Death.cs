using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerState_Death : PlayerState
{
    float tombstoneTime;
    MonoBehaviour mb;
    public PlayerState_Death(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        anim.Play("Death");
        idleDeath();
        mb = player.GetComponent<MonoBehaviour>();
        mb.StartCoroutine(waitTime());
    }
    IEnumerator waitTime(){
        yield return new WaitForSeconds(tombstoneTime);
        anim.Play("Death2");  
    }
    void idleDeath(){
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            //Debug.Log("HAHA" + clip.name);
            switch(clip.name){
                case "Player_Death":
                    tombstoneTime = clip.length;
                    break;
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
