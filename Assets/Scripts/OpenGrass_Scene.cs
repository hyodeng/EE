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
            NowStage.SetActive(false); // ���� ������
            selectStage.SetActive(true); // ���� â ������
            Repair1.GetComponent<Button>().interactable = true; // ����1 ��ư Ȱ��ȭ
            STAGE2.GetComponent<Button>().interactable = true; // �������� 2 ��ư Ȱ��ȭ
        }
    }
}
