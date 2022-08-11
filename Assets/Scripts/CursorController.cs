using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    private CursorControlls controls;

    private void Awake()
    {
        //InputAction Asset 가져오기
        controls = new CursorControlls();

        //커서이미지 변경
        ChangeCursor(cursor); 


        //Cursor.lockState = CursorLockMode.Confined; //게임 창 밖으로 마우스가 안나감

        //Cursor.lockState = CursorLockMode.Locked; // 마우스를 게임 중앙 좌표에 고정시키고 마우스커서가 안보임
        //Cursor.lockState = CursorLockMode.None; // 마우스커서 정상

    }


    public void OnEnable()
    {
        controls.Enable();
        //델리게이트여서
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.canceled += _ => EndedClick();
    }

    public void OnDisable()
    {
        controls.Mouse.Click.canceled -= _ => EndedClick();
        controls.Mouse.Click.started -= _ => StartedClick();
        controls.Disable();
    }


    public void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    public void EndedClick()
    {
        ChangeCursor(cursor);
    }

    
    public void ChangeCursor(Texture2D cursorType)
    {
        //hotspot : Cursor Texture의 어느 부분이 마우스의 좌표인지를 선택
        Vector2 hotSpot = new Vector2(cursorType.width /2, cursorType.height /2);   
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

}
