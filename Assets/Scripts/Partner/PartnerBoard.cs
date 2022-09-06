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
        popupController = GameObject.Find("PopupNextSceneController").gameObject.GetComponent<PopupController>();

    }

    private void Start()
    {
        Initialize();

        partnerListButton.onClick.AddListener(OnOffSelectBoard);

        popupController.OnPartnerCount += OpenPartnerCount;

    }

    void Initialize()
    {
        requestTxt.text = "동료를 선택하세요";
        if(GameManager.Inst.partnerCount < 4)
        {

            countTxt.text = GameManager.Inst.partnerCount.ToString();
        }
        else
        {
            Debug.Log("동료인원수 오류");
        }
        
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
        countTxt.text = GameManager.Inst.partnerCount.ToString();
        FindObjectOfType<PartnerSelectBoard>().OnCheck();
    }

}
