using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.anim.SetTrigger("Equip");
    }
    public override void UpdateState(ActionStateManager actions)
    {
        actions.SwitchState(actions.Default);
    }
}
