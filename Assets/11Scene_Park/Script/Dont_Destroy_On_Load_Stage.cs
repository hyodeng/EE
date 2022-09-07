using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont_Destroy_On_Load_Stage : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        this.gameObject.SetActive(false);
    }
}
