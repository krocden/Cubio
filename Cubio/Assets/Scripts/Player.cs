using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region  State Variables
    public PlayerStateMachine stateMachine { get; private set;} 
    public PlayerState_Idle IdleState { get; private set; }
    public PlayerState_Run RunState { get; private set; }
    public PlayerState_Air AirState { get; private set; }
    public PlayerState_Attack AttackState { get; private set; }
    public PlayerState_Duck DuckState { get; private set; }

    // [SerializeField]
    // private PlayerData playerData;
    #endregion 

    #region Components
    public Animator Anim { get; private set; }
    public SpriteRenderer Sprite { get; private set; }
    public PlayerInput InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC { get; private set; }
    [SerializeField] public Image healthBar;
    [SerializeField] public Image manaBar;
    [SerializeField] public LayerMask platformLayerMask;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public float movementSpeed = 3f;
    public float jumpForce = 14f;
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public int attackRangeLow = 12000;
    public int attackRangeHigh = 18000;
    public int critChance = 20;
    public float critDamage = 1.4f;
    //private Vector2 workspace;
    #endregion

    #region Placeholder Debug Variables
    //Placeholder
    public string showCurrentState;
    public string showCurrentVelocity;
    #endregion

    private void Awake(){
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerState_Idle(this, stateMachine, "Idle");
        RunState = new PlayerState_Run(this, stateMachine, "Run");
        AirState = new PlayerState_Air(this, stateMachine, "Air");
        AttackState = new PlayerState_Attack(this, stateMachine, "Attack");
        DuckState = new PlayerState_Duck(this, stateMachine, "Duck");
    }

    // Start is called before the first frame update
    void Start(){
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        BC = GetComponent<BoxCollider2D>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        
        //PE = GetComponent<PlatformEffector2D>();

        // Player starts in Idle state
        stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update(){
        //CurrentVelocity = RB.velocity;
        showCurrentState = stateMachine.CurrentState.ToString();
        showCurrentVelocity = RB.velocity.ToString();
        stateMachine.CurrentState.LogicUpdate();

        manaRegen();
        updateHealth();
        updateMana();
    }
    void FixedUpdate(){
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void manaRegen(){
        if(currentMana >= maxMana){
            currentMana = maxMana;
        } else {
            currentMana += 1;
        }
    }  
     void updateHealth(){
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }
    void updateMana(){
        manaBar.fillAmount = (float) currentMana / maxMana;
    }
    // public void SetVelocityX(float velocity){
    //     workspace.Set(velocity, CurrentVelocity.y);
    //     RB.velocity = workspace;
    //     CurrentVelocity = workspace;
    // }

    // void OnDrawGizmos(){
    //     Gizmos.color = Color.magenta;
    //     Gizmos.DrawWireSphere(BC.bounds.center + new Vector3(2.5f,0,0),2f);
    //     Gizmos.DrawWireSphere(BC.bounds.center - new Vector3(2.5f,0,0),2f);
    // }
}
