using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartnerBoard : MonoBehaviour
{

    TextMeshPro partnerSelect;
    TextMeshPro partnerCount;

    private void Awake()
    {
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
