using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal_Change_TO_Menu_Dessert : MonoBehaviour
{
    public GameObject stageScene;

    private void Awake()
    {
        stageScene = GetComponent<GameObject>();
    }

    private void Start()
    {
        if(stageScene == null)
        {
            stageScene = GameObject.FindWithTag("MainMenu");
        }
        else if(stageScene != null)
        {
            stageScene.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageScene.SetActive(true);
        }
    }
}
