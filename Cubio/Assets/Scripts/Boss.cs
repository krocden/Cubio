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

    State state;

    // Start is called before the first frame update
    void Start()
    {
        setHealthOnSpawn();

        state = State.Idle;

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
}
