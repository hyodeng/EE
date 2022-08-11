using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScences3 : MonoBehaviour
{
    public void ChangeScene()
    {
        Screen.SetResolution(200, 400, false);
        SceneManager.LoadScene("Run");
    }
}
