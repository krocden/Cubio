using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Handler : MonoBehaviour
{
    [SerializeField]
    List<GameObject> phaseList;
    [SerializeField]
    GameObject currentPhase;
    int currentPhaseNumber;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentPhaseNumber = 1;
        spawnBoss(0);
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
        if(!isDead){
            changePhase();
        }
    }

    void changePhase(){
        //Spawn next phase of boss (If exist) when current phase of boss is dead
        if(currentPhase == null){
            if(phaseList[currentPhaseNumber] != null){
                spawnBoss(currentPhaseNumber);
                currentPhaseNumber ++;
            } 
        }
    }
    void spawnBoss(int phase){
        currentPhase = phaseList[phase];
        currentPhase = Instantiate(phaseList[phase], transform.position, Quaternion.identity);
        currentPhase.transform.parent = transform;
    }

    void checkDeath(){
        if(currentPhase == null){
            if(currentPhaseNumber >= phaseList.Count){
                isDead = true;
                Destroy(gameObject);
            }
        }
    }

}
