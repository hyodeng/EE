using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PopupController : MonoBehaviour
{
    public GameObject questionPop;
    TextMeshProUGUI questionText;
    WaitForSeconds delayTime = new WaitForSeconds(2.0f);

    GameObject yesbutton;
    GameObject nobutton;
    Button savebutton;
    bool isNextScene = false;

    private void Awake()
    {
        questionText = questionPop.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        yesbutton = questionPop.transform.GetChild(1).gameObject;
        nobutton = questionPop.transform.GetChild(2).gameObject;
        savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
    }

    void Start()
    {
        Initialize();

        savebutton.onClick.AddListener(PopUpSaveQuestion);
        yesbutton.GetComponent<Button>().onClick.AddListener(SelectYes);
        nobutton.GetComponent<Button>().onClick.AddListener(OnOffSwitch);
    }



    private void Initialize()
    {
        questionPop.SetActive(false);
        yesbutton.SetActive(false);
        nobutton.SetActive(false);
        StartCoroutine(PopUpQuestion());
        StopCoroutine(PopUpQuestion());
    }

    IEnumerator PopUpQuestion()
    {
        
        yield return delayTime;
        questionText.text = "������ ������ ����\n ����� �÷��̾ �ٸ纸����.";
        questionText.fontSize = 45.0f;
        questionPop.SetActive(true);
        yield return delayTime;
        questionPop.SetActive(false);
        yesbutton.SetActive(true);
        nobutton.SetActive(true);
        questionText.fontSize = 36.0f;
    }

    public void OnOffSwitch()
    {
        if (questionPop.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    void Close()
    {
        questionPop.SetActive(false);
    }

    void Open()
    {
        questionPop.SetActive(true);
    }


    private void PopUpSaveQuestion()
    {
        OnOffSwitch();
        questionText.text = "������ �̹����� �����Ͻðڽ��ϱ�?";

        //Yes, No ��ư �׼��� ���� �Լ����� 
    }

    void SelectYes()
    {
        DataManager.Instance.SavePlayerData();
        isNextScene = true;
    }


    void PopUpNextScene()
    {
        if (isNextScene)
        {
            //ĳ���� Ŀ���͸����� _2��° ������ �̵�
            SceneManager.LoadScene("CharacterSelect");
        }
        else
        {
            OnOffSwitch();
            questionText.text = "ĳ���͸� �������� �ʾҽ��ϴ�.\nSAVE ��ư�� ���� ĳ���� ������ �������ּ���.";
            //��� �ִٰ� ����

            StartCoroutine(DelayNextStep());
        }
    }

    IEnumerator DelayNextStep()
    {
        yield return delayTime;
        OnOffSwitch();
    }
}
