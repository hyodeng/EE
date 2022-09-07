using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal_Change : MonoBehaviour
{
    public GameObject mainScene;
    public GameObject thisScene;
    public GameObject stageScene;
    private void Awake()
    {
        mainScene = GetComponent<GameObject>();
        thisScene = GetComponent<GameObject>();
        stageScene = GetComponent<GameObject>();
    }

    private void Start()
    {
        if(thisScene == null)
        {
            thisScene = GameObject.FindWithTag("Stage_1.5_Bg_Prefab");
        }

        if(mainScene == null)
        {
            mainScene = GameObject.Find("Main_Menu");
        }

        if(thisScene == null)
        {
            thisScene = GameObject.Find("Stage_Scene_DONTDESTROYONLOAD");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //    SceneManager.LoadScene("MainScene_1.5");
           
           thisScene.SetActive(false);
            
          
            mainScene.SetActive(true);
        }
        
    }
}
