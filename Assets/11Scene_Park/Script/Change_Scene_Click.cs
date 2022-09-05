using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Change_Scene_Click : MonoBehaviour
{
   public void ToStageSelect()
    {
        SceneManager.LoadScene("Stage_Scene");
    }

    public void ToCustomize()
    {
        SceneManager.LoadScene("CharacterCustomize");
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToStage_1()
    {
        SceneManager.LoadScene("Stage_1");
    }
    public void ToStage_2()
    {
        SceneManager.LoadScene("Stage_2");
    }

    public void ToStage_3()
    {
        SceneManager.LoadScene("Stage_3");
    }

    public void ToStage_DesertVillage()
    {
        SceneManager.LoadScene("Stage_1.5");
    }

    public void ToStage_CastleVillage()
    {
        SceneManager.LoadScene("Stage_2.5");
    }


    
    public void Quit()
    {
        Application.Quit();
    }

}
