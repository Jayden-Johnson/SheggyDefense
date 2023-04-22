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
        if(Input.GetKeyDown(KeyCode.W)) ExitState(movement,movement.Walk);
    }
    void ExitState(Movement movement, MovementBaseState state){
        movement.anim.SetBool("Emote",false);
        movement.SwitchState(state);
    }
        
}
