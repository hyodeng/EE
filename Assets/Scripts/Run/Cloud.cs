using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 10.0f;
    Rigidbody2D rigid = null;
   


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigid.velocity = (-transform.right * speed * Time.deltaTime);
    }

  
    


}
