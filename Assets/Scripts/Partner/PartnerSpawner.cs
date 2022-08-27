using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerSpawner : MonoBehaviour
{
    public GameObject partner;
    public bool OnSpawn;

    void Start()
    {
        GameObject Player = Instantiate(partner, Vector3.zero, Quaternion.identity);
        Player.name = "Character0";
        GameObject Partner = Instantiate(partner , transform.position, Quaternion.identity);
        Partner.name = "Character1";
        OnSpawn = true;
    }
}
