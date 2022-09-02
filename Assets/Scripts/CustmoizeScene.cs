using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustmoizeScene : MonoBehaviour
{

    public void CustomizedScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterCustomize");

    }
    public void SelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");

    }

    public void SelectPartnerScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelectPartner");
    }

    public void StageScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage_Scene");

    }



}
