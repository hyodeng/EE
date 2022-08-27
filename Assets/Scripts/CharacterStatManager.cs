using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterStatManager : MonoBehaviour
{
    Button nextScene;              //다음씬 선택 버튼
    private void Start()
    {
        nextScene = GameObject.Find("NextButton").GetComponent<Button>();
        nextScene.onClick.AddListener(() => SceneManager.LoadScene("CharacterSelect")); //addListener는 이벤트 발생 시마다 실행됨
    }
}
