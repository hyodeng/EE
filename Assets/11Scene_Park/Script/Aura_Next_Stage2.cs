using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aura_Next_Stage2 : MonoBehaviour
{
    //사막맵으로 이동
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Stage_2.5");
        }
        

    }
}
