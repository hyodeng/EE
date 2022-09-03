using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomize : MonoBehaviour
{
    public Button nextScene;              //다음씬 선택 버튼
    private void Start()
    {
        //nextScene = GameObject.Find("NextButton0").GetComponent<Button>();
        Customized custom = FindObjectOfType<Customized>();
        nextScene.onClick.AddListener(() => {
            for(int i = 0; i<10; i++)
            {
                GameManager.Inst.temp[i, 0] = custom.parts[i].sprite;
            }
            SceneManager.LoadScene("CharacterSelect");
            }); //addListener는 이벤트 발생 시마다 실행됨
    }
}
