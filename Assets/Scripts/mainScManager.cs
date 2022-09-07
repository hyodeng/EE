using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScManager : MonoBehaviour
{
    public GameObject minigame;
    public GameObject cooking;
    //public GameObject Village;

    public void pointEnter1()   //ui 버튼에 eventTrigger 추가해서 mouseEnter(마우스 커서 올렸을 때)
    {
        minigame.gameObject.SetActive(true);
    }

    public void pointExit1()    //ui 버튼에 eventTrigger 추가해서 mouseExit(마우스 커서 벗어났을 때)
    {
        minigame.gameObject.SetActive(false);
    }

    public void pointEnter2()
    {
        cooking.gameObject.SetActive(true);
    }

    public void pointExit2()
    {
        cooking.gameObject.SetActive(false);
    }

    //public void pointEnter3()
    //{
    //    Village.gameObject.SetActive(true);
    //}

    //public void pointExit3()
    //{
    //    Village.gameObject.SetActive(false);
    //}

    public void SceneChange()
    {
        SceneManager.LoadScene("FoodScene");
    }
}
