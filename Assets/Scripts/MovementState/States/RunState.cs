using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
   public override void EnterState(Movement movement)
    {
        movement.anim.SetBool("Running",true);
    }
    public override void UpdateState(Movement movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift) && InputManger.instance.canInput) ExitState(movement, movement.Walk);
        else if(movement.dir.magnitude < 1.0f) ExitState(movement, movement.Idle);
        
        if(movement.vInput<0) movement.currentMoveSpeed = movement.runBackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;
        if(Input.GetKeyDown(KeyCode.Space) && InputManger.instance.canInput){
            movement.previousState = this;
            ExitState(movement,movement.Jump);
        }
    }
    void ExitState(Movement movement, MovementBaseState state){
        movement.anim.SetBool("Running",false);
        movement.SwitchState(state);
    }
}
