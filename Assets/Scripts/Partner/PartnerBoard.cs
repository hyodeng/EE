using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartnerBoard : MonoBehaviour
{
    GameObject questionPop;
    TextMeshPro partnerSelect;
    TextMeshPro partnerCount;

    private void Awake()
    {
        questionPop = transform.Find("QuestionPop").gameObject;
        questionPop.SetActive(false);
        partnerSelect = transform.GetChild(0).GetComponent<TextMeshPro>();
        partnerCount = transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        partnerSelect.text = "���Ḧ �����߽��ϴ�";
        partnerCount.text = "1";
        //partnerSelect.text = "���� ������ �ο����� �Ѿ����ϴ�";
    }
}
