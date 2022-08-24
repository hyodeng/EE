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

    GameObject questionPop;
    public string popText;
    Button yesbutton;
    Button nobutton;
    Button selectbutton;


    //동료 인원수 : static 이나 추후 변경 필요
    public static uint partnerCount = 0;

    //델리게이트
    public Action OnPartnerSelectBoardOpen;
    public Action OnPartnerSelectBoardClose;
    public Action OnOffSwitch;

    private void Awake()
    {
        requestTxt = transform.GetChild(0).GetComponent<TextMeshPro>();
        countTxt = transform.GetChild(1).GetComponent<TextMeshPro>();

        //동료 선택 여부 물어보는 창
        questionPop = transform.Find("QustionPop").gameObject;
        yesbutton = transform.GetComponentsInChildren<Button>()[0];     //팝업창 : 동료 추가 yes버튼
        nobutton = transform.GetComponentsInChildren<Button>()[1];      //팝업창 : 동료 추가 no버튼
        selectbutton = transform.GetComponentsInChildren<Button>()[2];  //동료리스트 버튼 


        //왜 안되지?
        //yesbutton = questionPop.transform.GetChild(0).GetComponent<Button>();
        //nobutton = questionPop.transform.GetChild(1).GetComponent<Button>();

    }

    private void Start()
    {
        Initialize();

        yesbutton.onClick.AddListener(SelectPartner);
        nobutton.onClick.AddListener(NoSelectPartner);
        selectbutton.onClick.AddListener(OnOffSelectBoard);

        //addListener 두개하면 2개 메서드가 등록되나요?


        //StartCoroutine(PopPartnerBoard());
        //partnerSelect.text = "선택 가능한 인원수가 넘었습니다";
    }



    void Initialize()
    {
        requestTxt.text = "동료를 선택하세요";
        countTxt.text = partnerCount.ToString();
        questionPop.SetActive(false);

        StartCoroutine(PopUpQuestion());
    }



    public void OnOffSelectBoard()
    {
        OnOffSwitch?.Invoke();

    }


    IEnumerator PopUpQuestion()
    {
        yield return new WaitForSeconds(0.5f);
        questionPop.SetActive(true);

        questionPop.GetComponentInChildren<TextMeshProUGUI>().text = "여행을 함께할 동료를 선택할 수 있습니다.\n동료를 선택하시겠습니까?";
    }

    public void SelectPartner()
    {
        questionPop.SetActive(false);
        //선택된 동료모음창 오픈
        OnPartnerSelectBoardOpen?.Invoke(); 
    }

    private void NoSelectPartner()
    {
        questionPop.SetActive(false);
        OnPartnerSelectBoardClose?.Invoke();
    }

    //
    IEnumerator PopPartnerBoard()
    {
        yield return new WaitForSeconds(2.0f);
        requestTxt.text = "동료를 선택했습니다";
        countTxt.text = partnerCount.ToString();
    }

}
