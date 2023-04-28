using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteState : MovementBaseState
{
   public override void EnterState(Movement movement)
    {
        movement.Crouching();
        movement.anim.SetBool("Emote",true);
    }
    public override void UpdateState(Movement movement)
    {
        if (movement.dir.magnitude>0.1f) ExitState(movement, movement.Walk);
    }
    void ExitState(Movement movement, MovementBaseState state){
        movement.anim.SetBool("Emote",false);
        movement.unCrouching();
        movement.SwitchState(state);
    }
        
}
