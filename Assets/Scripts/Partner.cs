using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    
    public GameObject partner;

    private void Awake()
    {
        
    }

    void Start()
    {
        Instantiate(partner , transform.position, Quaternion.identity);
    }


}
