using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMenu : MonoBehaviour
{
    Button saveButton;
    Button cancelButton;
    ItemSlotUI itemSlotUI;

    private void Awake()
    {
        saveButton = transform.Find("SaveButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
        itemSlotUI = FindObjectOfType<ItemSlotUI>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        saveButton.onClick.AddListener(OnSaveButton);
        cancelButton.onClick.AddListener(OnCancelButton);
        itemSlotUI.OnOffPopup += OnOffSwitch;
    }


    private void OnCharacterSelected()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnOffSwitch()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

        }

    }

    void OnSaveButton() 
    {
        //저장 함수는 ItemSlotUI로 
        gameObject.SetActive(false);
    }

    void OnCancelButton()
    {
        gameObject.SetActive(false);
    }


}
