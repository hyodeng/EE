using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomize : MonoBehaviour
{
    private void Start()
    {
        Customized custom = FindObjectOfType<Customized>();
        {
            for (int i = 0; i < 10; i++)
            {
                GamaMan.Inst.temp[i, 0] = custom.parts[i].sprite;
            }

        }; //addListener는 이벤트 발생 시마다 실행됨
    }
}
