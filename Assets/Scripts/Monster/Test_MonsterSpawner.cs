using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MonsterSpawner : MonoBehaviour
{
    public GameObject goblin;
    public GameObject fiend;
    public GameObject golem;
    public GameObject darkLord;
    

    void Start()
    {
        for(int i = 0; i < 2; i++)
            goblin = Instantiate(goblin, new Vector3(2.0f * i, 2.0f,0), Quaternion.identity);

        fiend = Instantiate(fiend, new Vector3(2.0f, 0, 0), Quaternion.identity);

        golem = Instantiate(golem, new Vector3(2.0f, -2.0f, 0), Quaternion.identity);

        darkLord = Instantiate(darkLord, new Vector3(2.0f, -4.0f, 0), Quaternion.identity);



    }

    


}
