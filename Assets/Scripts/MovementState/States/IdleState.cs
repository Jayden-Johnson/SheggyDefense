using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    Animator anim;
    UnequipManager unequipManager;
    void Start(){
    }
   public override void EnterState(Movement movement)
    {
    }
    public override void UpdateState(Movement movement)
    {
        if(movement.dir.magnitude>0.1f){
            if(Input.GetKey(KeyCode.LeftShift))movement.SwitchState(movement.Run);
            else movement.SwitchState(movement.Walk);
        }
        if(Input.GetKeyDown(KeyCode.B)) {
            if(UnequipManager.instance.equiped) {
                UnequipManager.instance.equippedBeforeEmote = true;
                UnequipManager.instance.unEquip();
            } else {
                UnequipManager.instance.equippedBeforeEmote = false;
            }
            movement.SwitchState(movement.Emote);
        } 
        if(Input.GetKeyDown(KeyCode.C)) {
            movement.SwitchState(movement.Crouch);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            movement.previousState = this;
            movement.SwitchState(movement.Jump);
        }
    } 
}
