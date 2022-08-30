using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PartnerSelectBoard : MonoBehaviour
{
    PartnerBoard partnerBoard;

    public GameObject partner1;
    public GameObject partner2;
    public GameObject partner3;
    Image check1;

    public TextMeshProUGUI partnerNum1;
    public TextMeshProUGUI partnerName1;
    public TextMeshProUGUI PartnerExplanation1;

    public TextMeshProUGUI partnerNum2;
    public TextMeshProUGUI partnerName2;
    public TextMeshProUGUI PartnerExplanation2;

    public TextMeshProUGUI partnerNum3;
    public TextMeshProUGUI partnerName3;
    public TextMeshProUGUI PartnerExplanation3;


    private void Awake()
    {
        partnerBoard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();

        partner1 = transform.GetChild(3).gameObject;
        partner2 = transform.GetChild(4).gameObject;
        partner3 = transform.GetChild(5).gameObject;
        //체크 버튼 가져오기

        //동료1의 숫자, 직업, 설명
        partnerNum1 = partner1.GetComponentsInChildren<TextMeshProUGUI>()[0];
        partnerName1 = partner1.GetComponentsInChildren<TextMeshProUGUI>()[1];
        PartnerExplanation1 = partner1.GetComponentsInChildren<TextMeshProUGUI>()[2];
        

        //동료2의 숫자, 직업, 설명
        partnerNum2 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[0];
        partnerName2 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[1];
        PartnerExplanation2 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[2];

        //동료3의 숫자, 직업, 설명

        partnerNum3 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[0];
        partnerName3 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[1];
        PartnerExplanation3 = partner2.GetComponentsInChildren<TextMeshProUGUI>()[2];

    }

    private void Start()
    {
        this.gameObject.SetActive(false);
        
        //델리게이트 등록
        partnerBoard.OnPartnerSelectBoardOpen += Open;
        partnerBoard.OnPartnerSelectBoardClose += Close;   
        partnerBoard.OnOffSwitch += OnOffSwitch;
        
    }


    public void OnOffSwitch()
    {
        if (gameObject.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }


    public void Open()
    {
        if (this.gameObject.activeSelf == false)
            this.gameObject.SetActive(true);

        if(PartnerBoard.partnerCount == 0)
        {
            partner1.SetActive(false);
            partner2.SetActive(false);
            partner3.SetActive(false);

        }
        else if(PartnerBoard.partnerCount == 1)
        {
            partner1.SetActive(true);
            partner2.SetActive(false);
            partner3.SetActive(false) ;
        }
        else if(PartnerBoard.partnerCount == 2)
        {
            partner1.SetActive(true);
            partner2.SetActive(true);
            partner3.SetActive(false);
        }else if(PartnerBoard.partnerCount == 3)
        {

            partner1.SetActive(true);
            partner2.SetActive(true);
            partner3.SetActive(true);

        }
        else
        {
            Debug.Log("동료 카운트 오류");
        }
    }

    private void Close()
    {
        this.gameObject.SetActive(false);
    }

}
