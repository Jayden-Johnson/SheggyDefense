using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();
    public EmoteState Emote = new EmoteState();
    public JumpState Jump = new JumpState();
    public MovementBaseState previousState;
    [HideInInspector] public Animator anim;

    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 10, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;
    public float airSpeed = 1.5f;

    [HideInInspector] public Vector3 dir;

    CharacterController controller;
    [HideInInspector]public float hzInput, vInput;

    [SerializeField] float groundYOffset;
    [SerializeField]LayerMask groundMask;
    Vector3 spherePos;
    [SerializeField] float gravity =-9.81f;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Falling();

        anim.SetFloat("hzInput",hzInput);
        anim.SetFloat("vInput",vInput);
        currentState.UpdateState(this);
    }
    public void SwitchState(MovementBaseState state){
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove(){
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 airDir = Vector3.zero;
        if(!IsGrounded()) airDir = transform.forward * vInput + transform.right * hzInput;
        else dir = transform.forward * vInput + transform.right * hzInput;
        controller.Move((Vector3.ClampMagnitude(dir,1.0f)*currentMoveSpeed + airDir.normalized * airSpeed)*Time.deltaTime);

    }
    public bool IsGrounded(){
        spherePos = new Vector3(transform.position.x,transform.position.y * groundYOffset,transform.position.z);
        if(Physics.CheckSphere(spherePos, controller.radius -0.05f, groundMask)) return true;
        return false;
    }
    void Gravity(){
        if(!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }
    void Falling() => anim.SetBool("Falling",!IsGrounded());
    public void JumpForce() => velocity.y += jumpForce;
    public void Jumped() => jumped = true;
}
