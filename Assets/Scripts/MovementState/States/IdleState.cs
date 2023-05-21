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
            if(Input.GetKey(KeyCode.LeftShift) && InputManger.instance.canInput)movement.SwitchState(movement.Run);
            else movement.SwitchState(movement.Walk);
        }
        if(Input.GetKeyDown(KeyCode.B) && InputManger.instance.canInput) {
            movement.audioSource.Play();
            movement.audioSource.mute = false;
            if(UnequipManager.instance.equiped) {
                UnequipManager.instance.equippedBeforeEmote = true;
                UnequipManager.instance.unEquip();
            } else {
                UnequipManager.instance.equippedBeforeEmote = false;
            }
            if (UnequipManager.instance.equiped == false){
                movement.SwitchState(movement.Emote);
            }
            
        } 
        if(Input.GetKeyDown(KeyCode.C) && InputManger.instance.canInput) {
            movement.SwitchState(movement.Crouch);
        }
        if(Input.GetKeyDown(KeyCode.Space) && InputManger.instance.canInput) {
            movement.previousState = this;
            movement.SwitchState(movement.Jump);
        }
    } 
}
