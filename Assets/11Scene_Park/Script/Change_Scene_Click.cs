using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Change_Scene_Click : MonoBehaviour
{
    public Button stage1;
    public Button stage2;
    public Button stage3;
    public void Start()
    {
        stage1.interactable = GameManager.Inst.SceneUnlocked[0];
        stage2.interactable = GameManager.Inst.SceneUnlocked[1];
        stage3.interactable = GameManager.Inst.SceneUnlocked[2];
    }
    public void ToStageSelect()
    {
        SceneManager.LoadScene("Stage_Scene");
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Temp()
    {
        GameManager.Inst.Stage = 1;
        GameManager.Inst.SetMonsters = new string[4] { "Goblin", "Goblin", "Piend", "Golem" }; //Golbin, Piend, Golem, Darkload
        SceneManager.LoadScene("Battle_Scene");
    }
    public void ToStage_1()
    {
        GameManager.Inst.Stage = 1;
        GameManager.Inst.SetMonsters = new string[4] { "Goblin", "Goblin", "Piend", "Golem" }; //Golbin, Piend, Golem, Darkload
        SceneManager.LoadScene("Battle_Scene");
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
