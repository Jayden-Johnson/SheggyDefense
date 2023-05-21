using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : MonoBehaviour
{
    public bool canInput = true;

    public static InputManger instance;

    void Awake() {
        instance = this;
    }

    //InputManager.instance.canInput
}
