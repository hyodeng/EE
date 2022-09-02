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
        questionText.text = "여행을 떠나기 위해\n 당신의 플레이어를 꾸며보세요.";
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
        questionText.text = "현재의 이미지로 저장하시겠습니까?";

        //Yes, No 버튼 액션은 별도 함수에서 
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
            //캐릭터 커스터마이즈 _2번째 씬으로 이동
            SceneManager.LoadScene("CharacterSelect");
        }
        else
        {
            OnOffSwitch();
            questionText.text = "캐릭터를 저장하지 않았습니다.\nSAVE 버튼을 눌러 캐릭터 정보를 저장해주세요.";
            //잠깐 있다가 닫힘

            StartCoroutine(DelayNextStep());
        }
    }

    IEnumerator DelayNextStep()
    {
        yield return delayTime;
        OnOffSwitch();
    }
}
