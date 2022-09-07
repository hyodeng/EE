using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileScene : MonoBehaviour
{
    public void ToCustomize()
    {
        SceneManager.LoadScene("CharacterCustomize");
    }
}
