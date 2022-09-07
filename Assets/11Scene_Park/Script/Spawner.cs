using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Monsters;
    public GameObject[] Players;
    
    
    void Start()
    {
        if( Monsters.Length >= 4)
        {
            for(int i = 0; i < Monsters.Length; i++) {
                if (i == 3)
                {
                    Instantiate(Monsters[i], new Vector3(-1.0f, 0, -11.0f), Quaternion.Euler(10.0f, 0, 0));
                    Monsters[i].gameObject.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                }
                else 
                { 
                    Instantiate(Monsters[i], new Vector3(-4.0f + (i * 1.5f), 0, -9.0f - (i * 5.0f)), Quaternion.Euler(10.0f, 0, 0));
                    
                }

            }
        }

        else if( Monsters.Length == 3)
        {
            for(int i = 0; i < Monsters.Length; i++)
                Instantiate(Monsters[i], new Vector3(-4.0f + (i * 1.5f), 0, -9.0f - (i * 5.0f)), Quaternion.Euler(10.0f, 0, 0));
        }

        else if( Monsters.Length == 2)
        {
            for (int i = 0; i < Monsters.Length; i++)
                Instantiate(Monsters[i], new Vector3(-3.0f + (i * 1.5f), 0, -10.0f - (i * 5.0f)), Quaternion.identity);
        }
    }

   
}
