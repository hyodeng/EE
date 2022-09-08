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

    public AudioClip audioSave;
    public AudioClip audioYes;
    public AudioClip audioNo;
    AudioSource audioSource;


    TextMeshProUGUI questionText;
    WaitForSeconds delayTime0 = new WaitForSeconds(0.5f);
    WaitForSeconds delayTime1 = new WaitForSeconds(2.0f);

    GameObject yesbutton;
    GameObject nobutton;
    Button savebutton;
    Button nextButton;
    Button prevButton;
    bool isNextScene = false;
    public bool isReadySave = false;    //true일 때만 json으로 저장됨

    public System.Action OnEnabledpartnerSelectView;
    public System.Action OnPartnerCount;


    private void Awake()
    {
        questionText = questionPop.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        yesbutton = questionPop.transform.GetChild(1).gameObject;
        nobutton = questionPop.transform.GetChild(2).gameObject;
        savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        nextButton = nextSceneButton.GetComponent<Button>();
        prevButton = prevSceneButton.GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
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
        questionPop.SetActive(false);
        yesbutton.SetActive(false);
        nobutton.SetActive(false);

        if (questionPop.name == "QuestionPop_0")
        {
            StartCoroutine(PopUpQuestion());
            StopCoroutine(PopUpQuestion());

        }
        else if (questionPop.name == "QuestionPop_2")
        {
            StartCoroutine(PopUpPartnerSelectQuestion());
        }

    }

    IEnumerator PopUpQuestion()
    {
        yield return delayTime0;
        
        questionText.text = "여행을 떠나기 위해\n 당신의 플레이어를 꾸며보세요.";
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
        questionPop.GetComponentInChildren<TextMeshProUGUI>().text = "여행을 함께할 동료를 선택할 수 있습니다.\n동료를 선택하세요.";
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
        questionText.text = "현재의 캐릭터 정보로 저장하시겠습니까?";
        PlaySound("Save");
    }

    //팝업창의 yes 버튼을 눌렀을 때 
    void SelectYes()
    {

        PlaySound("Yes");
        if (questionPop.name == "QuestionPop_0")
        {
            //플레이어 파츠 저장
            OnOffSwitch();
            isNextScene = true;
        }
        else if (questionPop.name == "QuestionPop_1")
        {
            //플레이어의 데이터 저장
            isReadySave = true;
            if (isReadySave)
            {
                DataManager.Instance.SavePlayerParts();
                DataManager.Instance.SetPlayerToJson();
            }
            OnOffSwitch();
            isNextScene = true;

        }
        else if (questionPop.name == "QuestionPop_2")
        {
            //파트너 데이터와 파츠 저장
            isReadySave = true;
            if (isReadySave)
            {
                DataManager.Instance.SetPartnerToJson();
                DataManager.Instance.SavePartnerParts();
            }
            
            OnPartnerCount?.Invoke();
            OnOffSwitch();
            isNextScene = true;

        }

    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Save":
                audioSource.clip = audioSave;
                break;
            case "Yes":
                audioSource.clip = audioYes;
                break;
            case "No":
                audioSource.clip = audioNo;
                break;
            default:
                break;
        }
        audioSource.Play();
    }

    public void SelectNextScene()
    {
        if (isNextScene)
        {
            if (nextButton.name == "NextButton_0")
            {
                SceneManager.LoadScene("CharacterSelect");
            }
            else if (nextButton.name == "NextButton_1")
            {
                
                SceneManager.LoadScene("CharacterSelectPartner");
            }
            else if (nextButton.name == "NextButton_2")
            {

                SceneManager.LoadScene("Stage_Scene 1");
            }
            else
            {
                Debug.Log("씬 선택 오류");
            }
        }
        else
        {
            questionPop.SetActive(true);
            yesbutton.SetActive(false);
            nobutton.SetActive(false);

            questionText.text = "캐릭터를 저장하지 않았습니다.\nSAVE 버튼을 눌러 캐릭터 정보를 저장해주세요.";
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
            Debug.Log("기존 씬 선택 오류");
        }
    }



    //partnerSelect.text = "선택 가능한 인원수가 넘었습니다";


}
