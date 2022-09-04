using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene("Battle_Scene");
    }

}
