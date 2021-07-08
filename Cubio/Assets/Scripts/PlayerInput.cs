using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //public Vector2 MovementInput { get; private set; }

    public string buttonPressed;
    //public string buttonJumped;
    public float movementDir;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // Inputs
        movementDir = Input.GetAxisRaw("Horizontal");
        CheckInput();
    }
    void CheckInput(){
        // Key Inputs
        if(Input.GetKey(KeyCode.Z)){
            buttonPressed = "Z";
        } else if(Input.GetKey(KeyCode.X)){
            buttonPressed = "X";
        } else if(Input.GetKeyDown(KeyCode.Space)){
            buttonPressed = "JUMP";
        } else if(Input.GetKey(KeyCode.LeftArrow)){
           buttonPressed = "LEFT";
           //MovementInput = Vector2(dir * Player.movementSpeed, );
        } else if(Input.GetKey(KeyCode.RightArrow)){ 
           buttonPressed = "RIGHT";
        } else if(Input.GetKey(KeyCode.DownArrow)){
            buttonPressed = "DUCK";
        } else {
            buttonPressed = null;
        }

        
    }

    // public void OnMoveInput(InputAction.CallbackContext context){
    //     MovementInput = context.ReadValue<Vector2>();
    // }
    // public void onJumpInput(InputAction.CallbackContext context){

    // }
}
