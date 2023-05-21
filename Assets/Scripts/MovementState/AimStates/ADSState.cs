using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSState : AimBaseState
{
    public override void EnterState(AimStateManager aim){
        aim.anim.SetBool("Aiming",true);
        aim.currentFov = aim.adsfov;
    }

    public override void UpdateState(AimStateManager aim){
        if(Input.GetKeyUp(KeyCode.Mouse1) && InputManger.instance.canInput) aim.SwitchState(aim.Hip);
    }
}
