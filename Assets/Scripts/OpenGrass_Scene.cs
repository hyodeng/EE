using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGrass_Scene : MonoBehaviour
{
    public GameObject NowStage;
    public GameObject selectStage;
    public GameObject Repair1;
    public GameObject STAGE2;   
    public void stage2Open(float HP)
    {
        if(HP <= 0)
        {
            NowStage.SetActive(false); // 현재 닫히고
            selectStage.SetActive(true); // 선택 창 열리고
            Repair1.GetComponent<Button>().interactable = true; // 여관1 버튼 활성화
            STAGE2.GetComponent<Button>().interactable = true; // 스테이지 2 버튼 활성화
        }
    }
}
