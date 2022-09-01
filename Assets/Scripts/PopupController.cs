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
    public GameObject nextSceneButton;
    public GameObject prevSceneButton;
    Button nextButton;
    Button prevButton;

    TextMeshProUGUI questionText;
    WaitForSeconds delayTime0 = new WaitForSeconds(0.2f);
    WaitForSeconds delayTime1 = new WaitForSeconds(3.0f);

    GameObject yesbutton;
    GameObject nobutton;
    Button savebutton;
    bool isNextScene = false;
    public bool isReadySave = false;    //true�� ���� json���� �����

    //�����ο��� ����-----------------------------------------------------------
    const int MaxPartnerCount = 3;   //���� �ִ� �ο��� : 3�����
    int partnerCount;               //���� ���� �ο���
    public int PartnerCount
    {
        get => partnerCount;
        set
        {
            partnerCount = Mathf.Clamp(value, 0, MaxPartnerCount);
        }
    }
    //------------------------------------------------------------------------

    public System.Action OnEnabledpartnerSelectView;
    public System.Action OnPartnerBoardCount;

    private void Awake()
    {
        questionText = questionPop.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        yesbutton = questionPop.transform.GetChild(1).gameObject;
        nobutton = questionPop.transform.GetChild(2).gameObject;
        savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        nextButton = nextSceneButton.GetComponent<Button>();
        prevButton = prevSceneButton.GetComponent<Button>();
    }

    void Start()
    {

        Initialize();

        savebutton.onClick.AddListener(PopUpSaveQuestion);
        nextButton.onClick.AddListener(SelectNextScene);
        yesbutton.GetComponent<Button>().onClick.AddListener(SelectYes);
        nobutton.GetComponent<Button>().onClick.AddListener(OnOffSwitch);
        
    }



    private void Initialize()
    {
        partnerCount = 0; //�����ο��� �ʱ�ȭ
        questionPop.SetActive(false);
        yesbutton.SetActive(false);
        nobutton.SetActive(false);

        if(questionPop.name == "QuestionPop_0")
        {
            StartCoroutine(PopUpQuestion());
            StopCoroutine(PopUpQuestion());

        }else if(questionPop.name == "QuestionPop_2")
        {
            StartCoroutine(PopUpPartnerSelectQuestion());
        }
    }

    IEnumerator PopUpQuestion()
    {
        yield return delayTime0;
        questionText.text = "������ ������ ����\n ����� �÷��̾ �ٸ纸����.";
        questionText.fontSize = 45.0f;
        questionPop.SetActive(true);
        yield return delayTime1;
        questionPop.SetActive(false);
        yesbutton.SetActive(true);
        nobutton.SetActive(true);
        questionText.fontSize = 36.0f;
    }

    IEnumerator PopUpPartnerSelectQuestion()
    {
        yield return delayTime0;
        questionPop.GetComponentInChildren<TextMeshProUGUI>().text = "������ �Բ��� ���Ḧ ������ �� �ֽ��ϴ�.\n���Ḧ �����ϼ���.";
        questionPop.GetComponentInChildren<TextMeshProUGUI>().fontSize = 45.0f;
        questionPop.SetActive(true);
        yesbutton.SetActive(false);
        nobutton.SetActive(false);
        
        yield return delayTime1;
        questionPop.SetActive(false);
        yesbutton.SetActive(true);
        nobutton.SetActive(true);
        OnEnabledpartnerSelectView?.Invoke();
    }

    public void OnOffSwitch()
    {
        if (questionPop.activeSelf)
        {
            questionPop.SetActive(false);
            yesbutton.SetActive(false);
            nobutton.SetActive(false);
        }
        else
        {
            questionPop.SetActive(true);
            yesbutton.SetActive(true);
            nobutton.SetActive(true);
        }
    }


    private void PopUpSaveQuestion()
    {
        OnOffSwitch();
        questionText.text = "������ ĳ���� ������ �����Ͻðڽ��ϱ�?";
    }

    //�˾�â�� yes ��ư�� ������ �� 
    void SelectYes()
    {
        if(questionPop.name == "QuestionPop_0")
        {
            //�÷��̾� ���� ����
            DataManager.Instance.SavePlayerParts();
            OnOffSwitch();
            isNextScene = true;

        }
        else if(questionPop.name == "QuestionPop_1")
        {
            //�÷��̾��� ������ ����
            isReadySave = true;
            if (isReadySave)
            {
                DataManager.Instance.SavePlayerToJson();
            }
            OnOffSwitch();
            isNextScene = true;

        }
        else if(questionPop.name == "QuestionPop_2")
        {
            //���� ������ ����
            isReadySave = true;
            if (isReadySave)
            {
                DataManager.Instance.SavePartnerToJson();
            }
            //�����ο��� ����
            AddPartnerCount();
            OnPartnerBoardCount?.Invoke();
            OnOffSwitch();
            isNextScene = true;
        }
    }

    private void AddPartnerCount()
    {
        partnerCount++;
    }


    public void SelectNextScene()
    {
        if (isNextScene)
        {
            if(nextButton.name == "NextButton_0")
            {
                SceneManager.LoadScene("CharacterSelect");

            }else if(nextButton.name == "NextButton_1")
            {
                SceneManager.LoadScene("CharacterSelectPartner");
            }
            else if(nextButton.name == "NextButton_2")
            {
                SceneManager.LoadScene("Stage_Scene");
            }
            else
            {
                Debug.Log("�� ���� ����");
            }
        }
        else
        {
            questionPop.SetActive(true);
            yesbutton.SetActive(false);
            nobutton.SetActive(false);
            
            questionText.text = "ĳ���͸� �������� �ʾҽ��ϴ�.\nSAVE ��ư�� ���� ĳ���� ������ �������ּ���.";
            questionText.fontSize = 45.0f;
            StartCoroutine(DelayNextStep());
        }
    }


    IEnumerator DelayNextStep()
    {
        yield return delayTime1;
        questionPop.SetActive(false);
        yesbutton.SetActive(true);
        nobutton.SetActive(true);
        questionText.fontSize = 36.0f;

    }

    public void SelectPrevScene()
    {
        if (prevButton.name == "PreButton_1")
        {
            SceneManager.LoadScene("CharacterCustomize");

        }
        else if (prevButton.name == "PreButton_2")
        {
            SceneManager.LoadScene("CharacterSelect");
        }
        else
        {
            Debug.Log("���� �� ���� ����");
        }
    }


    //partnerSelect.text = "���� ������ �ο����� �Ѿ����ϴ�";


}
