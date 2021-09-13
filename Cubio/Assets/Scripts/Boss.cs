//using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : Monsters
{
    enum State {
        Idle,
        Attacking,
        Hit
    }

    public GameObject healthBar;
    State state;

    // Start is called before the first frame update
    void Start()
    {
        setHealthOnSpawn();

        state = State.Idle;

        if(healthBar != null){
            instantiateHealthBar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();

        switch(state){
            case State.Idle:
                idle();
                break;
            case State.Attacking:
                attacking();
                break;
        }
    }

    void phase(int currentPhase){
        if(currentPhase == 0){

        }
    }
    void idle(){

    }
    void attacking(){

    }
    void instantiateHealthBar(){
        var bossHealthBarVar = Instantiate(healthBar);
        bossHealthBarVar.transform.parent = gameObject.transform;
        var bossHP = bossHealthBarVar.transform.GetChild(0).GetChild(0);
        if(bossHP != null){
            var actualHealthBar = bossHP.GetComponent<Boss_HealthBar>();
            actualHealthBar.boss = gameObject.GetComponent<Boss>();
        }
        bossHealthBarVar.transform.parent = GameObject.Find("HUD").transform;
        
        Debug.Log("CURRENT POS "+bossHealthBarVar.transform.position);
        bossHealthBarVar.transform.position = new Vector3(0, 4.95f, 0);
        Debug.Log("CURRENT POS "+bossHealthBarVar.transform.position);
    }
}
