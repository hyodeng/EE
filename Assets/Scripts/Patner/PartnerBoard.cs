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


    //���� �ο��� : static �̳� ���� ���� �ʿ�
    public static uint partnerCount = 0;

    //��������Ʈ
    public Action OnPartnerSelectBoardOpen;
    public Action OnPartnerSelectBoardClose;
    public Action OnOffSwitch;

    private void Awake()
    {
        requestTxt = transform.GetChild(0).GetComponent<TextMeshPro>();
        countTxt = transform.GetChild(1).GetComponent<TextMeshPro>();

        //���� ���� ���� ����� â
        questionPop = transform.Find("QustionPop").gameObject;
        yesbutton = transform.GetComponentsInChildren<Button>()[0];     //�˾�â : ���� �߰� yes��ư
        nobutton = transform.GetComponentsInChildren<Button>()[1];      //�˾�â : ���� �߰� no��ư
        selectbutton = transform.GetComponentsInChildren<Button>()[2];  //���Ḯ��Ʈ ��ư 


        //�� �ȵ���?
        //yesbutton = questionPop.transform.GetChild(0).GetComponent<Button>();
        //nobutton = questionPop.transform.GetChild(1).GetComponent<Button>();

    }

    private void Start()
    {
        Initialize();

        yesbutton.onClick.AddListener(SelectPartner);
        nobutton.onClick.AddListener(NoSelectPartner);
        selectbutton.onClick.AddListener(OnOffSelectBoard);

        //addListener �ΰ��ϸ� 2�� �޼��尡 ��ϵǳ���?


        //StartCoroutine(PopPartnerBoard());
        //partnerSelect.text = "���� ������ �ο����� �Ѿ����ϴ�";
    }



    void Initialize()
    {
        requestTxt.text = "���Ḧ �����ϼ���";
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

        questionPop.GetComponentInChildren<TextMeshProUGUI>().text = "������ �Բ��� ���Ḧ ������ �� �ֽ��ϴ�.\n���Ḧ �����Ͻðڽ��ϱ�?";
    }

    public void SelectPartner()
    {
        questionPop.SetActive(false);
        //���õ� �������â ����
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
        requestTxt.text = "���Ḧ �����߽��ϴ�";
        countTxt.text = partnerCount.ToString();
    }

}
