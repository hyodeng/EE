using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_to_Select : MonoBehaviour
{
   public void StartToSelect()
    {
        SceneManager.LoadScene("Stage_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
