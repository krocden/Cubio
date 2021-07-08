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
    
    //public float hitStunTime;
    Animator animator;

    void Awake(){
        //textDamage = transform.GetComponent<TextMeshPro>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setHealthOnSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        //checkDeath();
    }

    public void hitMonster(int incomingDamage, int criticalChance, float critDamage){
        damageCalc(incomingDamage, criticalChance, critDamage);
    }

    void damageCalc(float incomingDamage, int criticalChance, float critDamage){
        float resistance = 1 - ((float)armorResist / 100);
        if(isCritical(criticalChance)){
            int damageReceived = (int)(critDamage * incomingDamage * resistance);
            GameObject textDamage = Instantiate(floatingDamage_Critical, transform.position, Quaternion.identity, this.gameObject.transform);
            textDamage.GetComponent<TextMesh>().text = damageReceived.ToString();
            currentHealth -= damageReceived;
        } else {
            int damageReceived = (int)(incomingDamage * resistance);
            GameObject textDamage = Instantiate(floatingDamage_Normal, transform.position, Quaternion.identity, this.gameObject.transform);
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

    protected void checkDeath(){
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }

    // void setup(int damageAmount){
    //     textDamage.SetText(damageAmount.ToString());
    // }

}
