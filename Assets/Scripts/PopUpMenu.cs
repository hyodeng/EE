using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMenu : MonoBehaviour
{
    Button selectButton;
    Button saveButton;
    Button cancelButton;


    private void Awake()
    {
        selectButton = GameObject.Find("SelectButton").GetComponent<Button>();
        saveButton = transform.Find("SaveButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();

    }

    private void Start()
    {
        gameObject.SetActive(false);
        selectButton.onClick.AddListener(OnCharacterSelected);
        saveButton.onClick.AddListener(OnSaveButton);
        cancelButton.onClick.AddListener(OnCancelButton);
    }


    private void OnCharacterSelected()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    void OnSaveButton() 
    {
        //Json 데이터로 저장
        SaveCharacterData();
        gameObject.SetActive(false);
    }

    void OnCancelButton()
    {
        gameObject.SetActive(false);
    }


    void SaveCharacterData()
    {
        //CharacterStat cs 파일 참조

    }

}
