using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HealthBar : MonoBehaviour
{
    Image healthBar;
    //[SerializeField]
    public Monsters boss;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float) boss.currentHealth / boss.maxHealth;
        checkDeath();
    }
    void checkDeath(){
        if(healthBar.fillAmount == 0){
            Destroy(this.transform.parent.parent.gameObject);
        }
    }
}
