using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float runSpeed = 6.0f;
    Animator anim;
    
    PlayerInputActions actions;
    Rigidbody rigid;

    Vector3 inputDir = Vector3.zero;
    

    private void Awake()
    {
       
        anim = GetComponent<Animator>();
        actions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;
    }
    private void OnDisable()
    {
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Player.Disable();
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + transform.forward * inputDir.y * runSpeed * Time.fixedDeltaTime);
    }
    void OnMoveInput(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        
    }

    
}
