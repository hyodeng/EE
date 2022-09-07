using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class All_About_Buttons : MonoBehaviour
{

    public GameObject SetMenu;
    public GameObject Mainscene;

    public void Stage_Desert_Village()
    {
        SceneManager.LoadScene("Stage_1.5");
    }

    public void Stage_Castle_Village()
    {
        SceneManager.LoadScene("Stage_2.5");
    }
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

    public void MainSceneON()
    {
        Mainscene.SetActive(true);
    }
    public void MainSceneOff()
    {
        Mainscene.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }

}

