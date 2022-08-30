using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Spawner : MonoBehaviour
{
    public GameObject prefab;
    
    void Start()
    {
        prefab = Instantiate(prefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
