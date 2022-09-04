using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Player : MonoBehaviour
{
    PlayerInputActions actions;
    public float MoveSpeed = 3.0f;
    Rigidbody rigid;
    Animator anim;
    Vector3 inputDir = Vector3.zero;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        actions = new PlayerInputActions();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        //actions.Player.Use.performed += OnUse;
        //actions.Player.Use.canceled += OnUse;
    }

    private void OnDisable()
    {
        //actions.Player.Use.canceled -= OnUse;
        //actions.Player.Use.performed -= OnUse;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;  
        actions.Player.Disable();   
    }
    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
        inputDir.Normalize();
    }
    void OnUse(InputAction.CallbackContext context)
    {
        anim.SetBool("Use", true);
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + MoveSpeed * Time.fixedDeltaTime * inputDir);
    }
}
