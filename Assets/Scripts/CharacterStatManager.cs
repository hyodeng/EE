using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterStatManager : MonoBehaviour
{
    Button nextScene;              //������ ���� ��ư
    private void Start()
    {
        nextScene = GameObject.Find("NextButton").GetComponent<Button>();
        nextScene.onClick.AddListener(() => SceneManager.LoadScene("CharacterSelect")); //addListener�� �̺�Ʈ �߻� �ø��� �����
    }
}
