using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartneSpawner : MonoBehaviour
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
