using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss : Monsters
{
    public bool hasSkill;
    public int spellDamage;
    public GameObject healthBar;
    AnimationClip[] clips;
    LayerMask playerLayer;
    PolygonCollider2D polygonCollider2D;
    float attackTime;
    float hitTime;
    float deathTime;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        setHealthOnSpawn();
        updateClipTime();
        if(hasSkill){
            Invoke("attack1", 5.0f);
        }
        if(healthBar != null){
            instantiateHealthBar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
        if(!dead){
            switch(state){
            case State.Idle:
                idle();
                break;
            case State.Attacking:
                attacking();
                break;
            case State.Hit:
                hit();
                break;
            }
        }
    }

    void phase(int currentPhase){
        if(currentPhase == 0){

        }
    }
    void idle(){
        //Debug.Log("PIANUS IDLE");
    }
    void attacking(){
        //Debug.Log("PIANUS ATTAK");
        StartCoroutine(waitTime(attackTime));
    }
    void hit(){
        //Debug.Log("PIANUS HIT");
        StartCoroutine(waitTime(hitTime));
    }
    IEnumerator waitTime(float clipTime){
        if(clipTime == 0){
        }
        else{
            float delay = clipTime/2.75f;
            yield return new WaitForSeconds(delay);
            if(state == State.Attacking){
                Vector2 startPos;
                startPos = new Vector2(polygonCollider2D.bounds.center.x, polygonCollider2D.bounds.center.y);
                Collider2D[] playerHit = Physics2D.OverlapAreaAll(startPos+ new Vector2(-3,-1f), startPos + new Vector2(-13,-2.8f), playerLayer);
                foreach(Collider2D player in playerHit){
                    player.GetComponent<Player>().hitByMonster(level, spellDamage);
                }
                //Debug.DrawLine(startPos+ new Vector2(-3,-1f), startPos + new Vector2(-13,-1f), Color.green);
                //Debug.DrawLine(startPos+ new Vector2(-3,-2.8f), startPos + new Vector2(-13,-2.8f), Color.magenta);
                //Debug.DrawLine(startPos+ new Vector2(-3,-1f), startPos + new Vector2(-13,-2.8f), Color.cyan);
            }
            yield return new WaitForSeconds(clipTime - delay);
        }
            state = State.Idle;
    }
    void attack1(){
        float randomTime = Random.Range(6.0f,14.0f);
        Invoke("attack1", randomTime);
        state = State.Attacking;
        animator.Play("Attack");
        
    }
    void updateClipTime(){
        clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            switch(clip.name){
                case "Pianus_Attack":
                    attackTime = clip.length;
                    break;
                case "Pianus_Hit":
                    hitTime = clip.length;
                    break;
                case "Pianus_Death":
                    deathTime = clip.length;
                    break;
            }
        }
    }
    void instantiateHealthBar(){
        var bossHealthBarVar = Instantiate(healthBar);
        //bossHealthBarVar.transform.parent = gameObject.transform;
        var bossHP = bossHealthBarVar.transform.GetChild(0).GetChild(0);
        if(bossHP != null){
            var actualHealthBar = bossHP.GetComponent<Boss_HealthBar>();
            actualHealthBar.boss = gameObject.GetComponent<Boss>();
        }
        bossHealthBarVar.transform.parent = GameObject.Find("HUD").transform;
        Camera c =  GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector2 v = c.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, c.transform.position.z));
        bossHealthBarVar.transform.position = new Vector3(0, v.y - 0.75f, 0);
    }

    public override void checkDeath(){
        if(currentHealth <= 0){
            dead = true;
            animator.Play("Death");
            checkDamageChild();
            StartCoroutine(deathTimer());
        }
    }
    IEnumerator deathTimer(){
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
