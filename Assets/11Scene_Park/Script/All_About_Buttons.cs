using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class All_About_Buttons : MonoBehaviour
{

    public GameObject SetMenu;
    public void Stage_One()
    {
        SceneManager.LoadScene("Stage_1");           
    }
    public void Stage_Two()
    {
        SceneManager.LoadScene("Stage_2");
    }
    public void Stage_Boss()
    {
        SceneManager.LoadScene("Stage_3");
    }
    public void To_Select()
    {
        SceneManager.LoadScene("Stage_Scene");
    }

    public void To_Start()
    {
        SceneManager.LoadScene("Start_Scene");
    }

    public void OnMenuSet()
    {
        SetMenu.SetActive(true);
    }
    public void OffMenuSet()
    {
        SetMenu.SetActive(false);
    }

    public void GameExit()
    {
        Application.Quit();
    }

}

