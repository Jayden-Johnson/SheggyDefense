using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.anim.SetTrigger("Unequip");
    }
    public override void UpdateState(ActionStateManager actions)
    {
        actions.SwitchState(actions.Default);
    }
}
