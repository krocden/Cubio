using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monsters : MonoBehaviour
{
    //public string Name;
    public int maxHealth;
    public int currentHealth;
    public int level;
    public float expMultiplier; 
    public float damageMultiplier;
    public int armorResist;
    //TextMeshPro textDamage;
    public GameObject floatingDamage_Normal;
    public GameObject floatingDamage_Critical;
    public enum State {
        Idle,
        Attacking,
        Hit
    }
    public State state;
    public float hitStunTime;
    public Animator animator;

    void Awake(){
        //textDamage = transform.GetComponent<TextMeshPro>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        setHealthOnSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        //checkDeath();
    }

    public void hitMonster(int skillDamage, int attackRangeLow, int attackRangeHigh, int damageLines, int criticalChance, float critDamage){
        if(state != State.Attacking){
            animator.Play("Hit");
            state = State.Hit;
        }
        for(int i = 0; i < damageLines; i++){
            int damageSend = (skillDamage/100) * Random.Range(attackRangeLow, attackRangeHigh);
            Debug.Log("SENT  \""+this.name+"\" "+ damageSend + " DAMAGE");
            damageCalc(damageSend, criticalChance, critDamage, i);
        }
    }

    void damageCalc(float incomingDamage, int criticalChance, float critDamage, int currentLine){
        float resistance = 1 - ((float)armorResist / 100);
        if(isCritical(criticalChance)){
            int damageReceived = (int)(critDamage * incomingDamage * resistance);
            GameObject textDamage = Instantiate(floatingDamage_Critical, transform.position + new Vector3(0, 2, 0), Quaternion.identity, this.gameObject.transform);
            textDamage.transform.position += new Vector3(0, (float)1, 0) * currentLine;
            textDamage.GetComponent<TextMesh>().text = damageReceived.ToString();
            currentHealth -= damageReceived;
        } else {
            int damageReceived = (int)(incomingDamage * resistance);
            GameObject textDamage = Instantiate(floatingDamage_Normal, transform.position + new Vector3(0, 2, 0), Quaternion.identity, this.gameObject.transform);
            textDamage.transform.position += new Vector3(0, (float)1, 0) * currentLine;
            textDamage.GetComponent<TextMesh>().text = damageReceived.ToString();
            currentHealth -= damageReceived;
        }
    }

    bool isCritical(int criticalChance){
        int crit = Random.Range(0,100);
        if(criticalChance >= crit){
            return true;
        }
        return false;
    }

    protected void setHealthOnSpawn(){
        currentHealth = maxHealth;
    }

    public virtual void checkDeath(){
        if(currentHealth <= 0){
            checkDamageChild();
            Destroy(gameObject);
        }
    }
    protected void checkDamageChild(){
        foreach (Transform child in transform) {
            child.parent = null;
        }
    }

    // void setup(int damageAmount){
    //     textDamage.SetText(damageAmount.ToString());
    // }

}
