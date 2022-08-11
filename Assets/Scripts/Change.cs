using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    //bool open = false;
    public GameObject stage_One = null;
    public GameObject stage_Two = null;
    public GameObject stage_Boss = null;

    public GameObject stage_Select = null;
    public GameObject Enemy = null;

    

    


    private void Start()
    {
        stage_One.SetActive(false);
        stage_Two.SetActive(false);
        stage_Boss.SetActive(false);
        stage_Select.SetActive(true);    
    }

    public void Stage_One()
    {
        //SceneManager.LoadScene("Grass_Scene");
        stage_Select.SetActive(false);
        stage_One.SetActive(true); // 1 스테이지로 이        
    }
    public void Stage_Two()
    {
       stage_Select.SetActive(false);
       stage_Two.SetActive(true);
       // Debug.Log("인식됨");
       
    }
    public void Stage_Boss()
    {
       stage_Select.SetActive(false);
       stage_Boss.SetActive(true);
       // //Instantiate(stage_Boss);
    }
    public void StageSelect()
    {
        //Instantiate(stage_Select);
    }

    public void SelectToStart()
    {
        SceneManager.LoadScene("Start_Scene");
    }

    
}

