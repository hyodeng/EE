using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PartnerSelectBoard : MonoBehaviour
{
    public GameObject partner1;
    public GameObject partner2;
    public GameObject partner3;
    Button cancelButton;

    PartnerBoard partnerBoard;
    PartnerSelectView partnerSelectView;

    private void Awake()
    {
        partnerBoard = GameObject.Find("PartnerBoard").GetComponent<PartnerBoard>();
        partnerSelectView = GameObject.Find("PartnerSelectView").GetComponent<PartnerSelectView>();

        partner1 = transform.GetChild(3).gameObject;
        partner2 = transform.GetChild(4).gameObject;
        partner3 = transform.GetChild(5).gameObject;

        cancelButton = transform.GetComponentInChildren<Button>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);

        //델리게이트 등록
        partnerBoard.onPartnerSelectBoardOpen += Open;
        partnerBoard.onPartnerSelectBoardClose += Close;
        partnerBoard.onOffSwitch += OnOffSwitch;

        partnerSelectView.onPartnerSelectBoard += Open;
        partnerSelectView.offPartnerSelectBoard += Close;

        cancelButton.onClick.AddListener(Close);
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

        if (GameManager.Inst.partnerCount == 0 || GameManager.Inst.partnerCount == 1)
        {
            partner1.SetActive(true);
            partner2.SetActive(false);
            partner3.SetActive(false);
            OffCheck();

        }
        else if (GameManager.Inst.partnerCount == 2)
        {
            partner1.SetActive(true);
            partner2.SetActive(true);
            partner3.SetActive(false);
            OffCheck();

        }
        else if (GameManager.Inst.partnerCount == 3)
        {

            partner1.SetActive(true);
            partner2.SetActive(true);
            partner3.SetActive(true);
            OffCheck();

        }
        else
        {
            Debug.Log("동료 카운트 오류");
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void OffCheck()
    {
        partner1.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.clear;
        partner2.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.clear;
        partner3.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.clear;
    }

    public void OnCheck()
    {
        if(GameManager.Inst.partnerCount == 1)
        {
            partner1.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.white;

        }else if(GameManager.Inst.partnerCount == 2)
        {
            partner2.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.white;

        }
        else if(GameManager.Inst.partnerCount == 3)
        {
            partner3.transform.Find("Check").gameObject.GetComponent<Image>().color = Color.white;

        }
    }
}
