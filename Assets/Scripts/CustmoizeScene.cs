using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustmoizeScene : MonoBehaviour
{
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
    }
}
