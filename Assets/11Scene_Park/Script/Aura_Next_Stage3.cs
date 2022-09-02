using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aura_Next_Stage2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Use"))
        {
            SceneManager.LoadScene("Stage_2");
        }
    }
}
