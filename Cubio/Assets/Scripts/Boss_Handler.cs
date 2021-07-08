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
    // Start is called before the first frame update
    void Start()
    {
        currentPhaseNumber = 1;
        spawnBoss(0);
    }

    // Update is called once per frame
    void Update()
    {
        changePhase();
    }

    void changePhase(){
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

}
