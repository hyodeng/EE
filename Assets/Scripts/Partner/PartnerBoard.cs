using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PartnerBoard : MonoBehaviour
{
    TextMeshPro requestTxt;
    TextMeshPro countTxt;

    Button partnerListButton; //동료리스트 버튼

    //동료 인원수 : static 이나 추후 변경 필요
    public static uint partnerCount = 0;

    //델리게이트
    //아래 2개 삭제 해야하나??
    public Action onPartnerSelectBoardOpen;
    public Action onPartnerSelectBoardClose;
    public Action onOffSwitch;

    PopupController popupController;

    WaitForSeconds delayTime = new WaitForSeconds(0.5f);

    private void Awake()
    {
        requestTxt = transform.GetChild(0).GetComponent<TextMeshPro>();
        countTxt = transform.GetChild(1).GetComponent<TextMeshPro>();

        partnerListButton = transform.GetComponentInChildren<Button>();
        popupController = GameObject.Find("PopupController").gameObject.GetComponent<PopupController>();

    }

    private void Start()
    {
        Initialize();

        partnerListButton.onClick.AddListener(OnOffSelectBoard);
        //addListener 두개하면 2개 메서드가 등록되나요?

        popupController.OnPartnerCount += OpenPartnerCount;
    }



    void Initialize()
    {
        requestTxt.text = "동료를 선택하세요";
        countTxt.text = partnerCount.ToString();
        StartCoroutine(PopUpQuestion());
    }
    IEnumerator PopUpQuestion()
    {
        yield return delayTime;
    }


    //동료리스트버튼 : PartnerSelectBoard 온오프 스위치
    public void OnOffSelectBoard()
    {
        onOffSwitch?.Invoke();
    }

    private void OpenPartnerCount()
    {
        StartCoroutine(OpenPartnerBoard());
    }

    IEnumerator OpenPartnerBoard()
    {
        yield return delayTime;
        requestTxt.text = "동료를 선택했습니다";
        countTxt.text = partnerCount.ToString();
    }

}
