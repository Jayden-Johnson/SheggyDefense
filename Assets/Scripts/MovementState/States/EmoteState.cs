using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteState : MovementBaseState
{
   public override void EnterState(Movement movement)
    {
        movement.anim.SetBool("Emote",true);
    }

    public override void UpdateState(Movement movement)
    {
        if (movement.dir.magnitude>0.1f){
        ExitState(movement, movement.Walk);
        movement.audioSource.mute = true;
        movement.audioSource.Stop();
        movement.audioSource.time = 0f;
        } 

    }

    void ExitState(Movement movement, MovementBaseState state)
    {
        if (UnequipManager.instance.equippedBeforeEmote) {
            UnequipManager.instance.equip();
        }
        movement.anim.SetBool("Emote",false);
        movement.SwitchState(state);
    }     
}
