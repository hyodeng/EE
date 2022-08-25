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

        partnerSelect.text = "동료를 선택했습니다";
        partnerCount.text = "1";

        //partnerSelect.text = "선택 가능한 인원수가 넘었습니다";
    }

}
