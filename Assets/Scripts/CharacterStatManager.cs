using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterStatManager : MonoBehaviour
{
    public static CharacterStatManager instance;

    Button nextScene;              //다음씬 선택 버튼
    int sceneIndex = 0;            //test

    public Button nextScene_2;
    int index = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        nextScene = GameObject.Find("NextButton").GetComponent<Button>();
        nextScene.onClick.AddListener(OnStageStart); //addListener는 이벤트 발생 시마다 실행됨

    }


    void OnStageStart()
    {
        //추가 : select 버튼을 누른 후 캐릭터를 저장하지 않고 next 버튼을 누르면 오류 메시지가 뜨도록 함

        //수정 : 전체 씬을 모아서 이름이나 index 정렬 필요
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnNextStage(int index)
    {
        SceneManager.LoadScene(index);
    }

}
