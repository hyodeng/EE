using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerSpawner : MonoBehaviour
{
    public GameObject partner;
    public bool OnSpawn;

    private void Awake()
    {
        
    }

    void Start()
    {
        GameObject Partner = Instantiate(partner , transform.position, Quaternion.identity);
        Partner.name = "Partner";
        OnSpawn = true;
    }
}
